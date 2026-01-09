using FCG.Application.DTOs;
using FCG.Application.Interfaces.Services;
using FCG.Domain.Interfaces;
using FCG.Domain.Models;

namespace FCG.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogService _logService;

        public TransactionService(ITransactionRepository transactionRepository, ILogService logService)
        {
            _transactionRepository = transactionRepository;
            _logService = logService;
        }

        public async Task<TransactionDTO> ProcessPaymentAsync(TransactionDTO transactionDTO)
        {
            if (transactionDTO.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            if (string.IsNullOrWhiteSpace(transactionDTO.PaymentMethod))
                throw new ArgumentException("Payment method is required");

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid().ToString(),
                Amount = transactionDTO.Amount,
                Currency = transactionDTO.Currency,
                PaymentMethod = transactionDTO.PaymentMethod,
                Description = transactionDTO.Description,
                OrderId = transactionDTO.OrderId,
                CustomerId = transactionDTO.CustomerId,
                Reference = transactionDTO.Reference,
                Status = "Processing",
                CreatedAt = DateTime.UtcNow,
                RetryCount = 0
            };

            await _transactionRepository.Add(transaction);

            await LogAsync("Info", $"Payment processing started for transaction {transaction.TransactionId} - Amount: {transaction.Amount} {transaction.Currency}");

            return MapToDTO(transaction);
        }

        public async Task<TransactionDTO> GetTransactionAsync(int id)
        {
            var transaction = await _transactionRepository.GetById(id);
            if (transaction == null)
                throw new KeyNotFoundException("Transaction not found");

            return MapToDTO(transaction);
        }

        public async Task<TransactionDTO?> GetTransactionByIdAsync(string transactionId)
        {
            var transaction = await _transactionRepository.GetByTransactionIdAsync(transactionId);
            return transaction == null ? null : MapToDTO(transaction);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByStatusAsync(string status)
        {
            var transactions = await _transactionRepository.GetByStatusAsync(status);
            return transactions.Select(MapToDTO);
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAll();
            return transactions.Select(MapToDTO);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByCustomerIdAsync(string customerId)
        {
            var transactions = await _transactionRepository.GetByCustomerIdAsync(customerId);
            return transactions.Select(MapToDTO);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var transactions = await _transactionRepository.GetByDateRangeAsync(startDate, endDate);
            return transactions.Select(MapToDTO);
        }

        public async Task<TransactionDTO> UpdateTransactionStatusAsync(int id, string status)
        {
            var validStatuses = new[] { "Pending", "Processing", "Completed", "Failed", "Cancelled" };
            if (!validStatuses.Contains(status))
                throw new ArgumentException($"Invalid status. Valid statuses are: {string.Join(", ", validStatuses)}");

            var transaction = await _transactionRepository.GetById(id);
            if (transaction == null)
                throw new KeyNotFoundException("Transaction not found");

            var oldStatus = transaction.Status;
            transaction.Status = status;
            transaction.UpdatedAt = DateTime.UtcNow;

            if (status == "Completed")
                transaction.ProcessedAt = DateTime.UtcNow;

            await _transactionRepository.Update(transaction);

            await LogAsync("Info", $"Transaction {transaction.TransactionId} status changed from {oldStatus} to {status}");

            return MapToDTO(transaction);
        }

        public async Task<bool> CancelTransactionAsync(int id)
        {
            var transaction = await _transactionRepository.GetById(id);
            if (transaction == null)
                return false;

            if (transaction.Status == "Completed")
                throw new InvalidOperationException("Cannot cancel a completed transaction");

            transaction.Status = "Cancelled";
            transaction.UpdatedAt = DateTime.UtcNow;

            await _transactionRepository.Update(transaction);
            await LogAsync("Info", $"Transaction {transaction.TransactionId} cancelled");

            return true;
        }

        private TransactionDTO MapToDTO(Transaction transaction)
        {
            return new TransactionDTO
            {
                Id = transaction.Id,
                TransactionId = transaction.TransactionId,
                Amount = transaction.Amount,
                Currency = transaction.Currency,
                Status = transaction.Status,
                PaymentMethod = transaction.PaymentMethod,
                Description = transaction.Description,
                OrderId = transaction.OrderId,
                CustomerId = transaction.CustomerId,
                Reference = transaction.Reference,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt,
                ProcessedAt = transaction.ProcessedAt,
                FailureReason = transaction.FailureReason,
                RetryCount = transaction.RetryCount
            };
        }

        private async Task LogAsync(string level, string message)
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Message = message,
                Level = level,
                Source = "TransactionService"
            };

            await _logService.SaveAsync(logEntry);
        }
    }
}

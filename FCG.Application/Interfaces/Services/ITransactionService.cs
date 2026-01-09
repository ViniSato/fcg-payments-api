using FCG.Application.DTOs;

namespace FCG.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<TransactionDTO> ProcessPaymentAsync(TransactionDTO transactionDTO);
        Task<TransactionDTO> GetTransactionAsync(int id);
        Task<TransactionDTO?> GetTransactionByIdAsync(string transactionId);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByStatusAsync(string status);
        Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync();
        Task<IEnumerable<TransactionDTO>> GetTransactionsByCustomerIdAsync(string customerId);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<TransactionDTO> UpdateTransactionStatusAsync(int id, string status);
        Task<bool> CancelTransactionAsync(int id);
    }
}

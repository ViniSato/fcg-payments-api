using FCG.Api.Models.Requests;
using FCG.Api.Models.Responses;
using FCG.Application.DTOs;

namespace FCG.Api.Services.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDTO ToDTO(TransactionRequest request)
        {
            return new TransactionDTO
            {
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethod = request.PaymentMethod,
                Description = request.Description,
                OrderId = request.OrderId,
                CustomerId = request.CustomerId,
                Reference = request.Reference,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static TransactionResponse ToResponse(TransactionDTO dto)
        {
            return new TransactionResponse
            {
                Id = dto.Id,
                TransactionId = dto.TransactionId,
                Amount = dto.Amount,
                Currency = dto.Currency,
                Status = dto.Status,
                PaymentMethod = dto.PaymentMethod,
                Description = dto.Description,
                OrderId = dto.OrderId,
                CustomerId = dto.CustomerId,
                Reference = dto.Reference,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                ProcessedAt = dto.ProcessedAt,
                FailureReason = dto.FailureReason,
                RetryCount = dto.RetryCount
            };
        }
    }
}

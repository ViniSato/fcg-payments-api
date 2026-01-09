namespace FCG.Application.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "BRL";
        public string Status { get; set; } = "Pending";
        public string PaymentMethod { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? Reference { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public string? FailureReason { get; set; }
        public int RetryCount { get; set; }
    }
}

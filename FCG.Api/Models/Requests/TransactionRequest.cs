namespace FCG.Api.Models.Requests
{
    public class TransactionRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "BRL";
        public string PaymentMethod { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? Reference { get; set; }
    }
}

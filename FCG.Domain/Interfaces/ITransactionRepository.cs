using FCG.Domain.Models;

namespace FCG.Domain.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<Transaction?> GetByTransactionIdAsync(string transactionId);
        Task<IEnumerable<Transaction>> GetByStatusAsync(string status);
        Task<IEnumerable<Transaction>> GetByCustomerIdAsync(string customerId);
        Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}

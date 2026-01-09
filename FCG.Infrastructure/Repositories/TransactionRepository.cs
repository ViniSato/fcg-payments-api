using FCG.Domain.Interfaces;
using FCG.Domain.Models;
using FCG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FCG.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly FCGContext _context;

        public TransactionRepository(FCGContext context, ILogger<BaseRepository<Transaction>> logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<Transaction?> GetByTransactionIdAsync(string transactionId)
        {
            return await _context.Set<Transaction>()
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        public async Task<IEnumerable<Transaction>> GetByStatusAsync(string status)
        {
            return await _context.Set<Transaction>()
                .Where(t => t.Status == status)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByCustomerIdAsync(string customerId)
        {
            return await _context.Set<Transaction>()
                .Where(t => t.CustomerId == customerId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Set<Transaction>()
                .Where(t => t.CreatedAt >= startDate && t.CreatedAt <= endDate)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}

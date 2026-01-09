using FCG.Domain.Models;
using FCG.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Context
{
    public class FCGContext : DbContext
    {
        public FCGContext(DbContextOptions<FCGContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TransactionMap.Map(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}

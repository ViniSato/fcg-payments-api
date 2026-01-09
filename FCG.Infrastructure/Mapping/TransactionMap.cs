using FCG.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Mapping
{
    public static class TransactionMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions");

            modelBuilder.Entity<Transaction>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.TransactionId)
                .HasColumnName("TransactionId")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(10)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Status)
                .HasColumnName("Status")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(x => x.PaymentMethod)
                .HasColumnName("PaymentMethod")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Description)
                .HasColumnName("Description")
                .HasMaxLength(500);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.OrderId)
                .HasColumnName("OrderId")
                .HasMaxLength(100);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .HasMaxLength(100);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Reference)
                .HasColumnName("Reference")
                .HasMaxLength(200);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(x => x.UpdatedAt)
                .HasColumnName("UpdatedAt");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.ProcessedAt)
                .HasColumnName("ProcessedAt");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.FailureReason)
                .HasColumnName("FailureReason")
                .HasMaxLength(500);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.RetryCount)
                .HasColumnName("RetryCount")
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .HasIndex(x => x.TransactionId)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .HasIndex(x => x.Status);

            modelBuilder.Entity<Transaction>()
                .HasIndex(x => x.CustomerId);

            modelBuilder.Entity<Transaction>()
                .HasIndex(x => x.CreatedAt);
        }
    }
}

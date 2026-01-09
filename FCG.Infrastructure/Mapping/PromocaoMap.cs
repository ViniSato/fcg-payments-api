using FCG.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Mapping
{
    public static class PromocaoMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promocao>()
                .ToTable("Promocoes");

            modelBuilder.Entity<Promocao>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Promocao>()
                .Property(x => x.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Promocao>()
                .Property(x => x.JogoId)
                .HasColumnName("JogoId");

            modelBuilder.Entity<Promocao>()
                .Property(x => x.DescontoPercentual)
                .HasColumnName("DescontoPercentual")
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Promocao>()
                .Property(x => x.DataInicio)
                .HasColumnName("DataInicio");

            modelBuilder.Entity<Promocao>()
                .Property(x => x.DataFim)
                .HasColumnName("DataFim");

            modelBuilder.Entity<Promocao>()
                .HasOne(x => x.Jogo)
                .WithMany(j => j.Promocoes)
                .HasForeignKey(x => x.JogoId);
        }
    }
}

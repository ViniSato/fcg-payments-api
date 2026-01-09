using FCG.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Mapping
{
    public static class JogoMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogo>()
                .ToTable("Jogos");

            modelBuilder.Entity<Jogo>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Jogo>()
                .Property(x => x.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Jogo>()
                .Property(x => x.Titulo)
                .HasColumnName("Titulo")
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Jogo>()
                .Property(x => x.Descricao)
                .HasColumnName("Descricao");

            modelBuilder.Entity<Jogo>()
                .Property(x => x.Genero)
                .HasColumnName("Genero")
                .HasMaxLength(100);

            modelBuilder.Entity<Jogo>()
                .Property(x => x.Preco)
                .HasColumnName("Preco")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            modelBuilder.Entity<Jogo>()
                .Property(x => x.DataLancamento)
                .HasColumnName("DataLancamento");

            modelBuilder.Entity<Jogo>()
                .Property(x => x.CriadoEm)
                .HasColumnName("CriadoEm");

            modelBuilder.Entity<Jogo>()
                .Property(x => x.AtualizadoEm)
                .HasColumnName("AtualizadoEm");
        }
    }
}
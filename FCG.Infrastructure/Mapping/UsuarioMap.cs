using FCG.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Mapping
{
    public static class UsuarioMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios");

            modelBuilder.Entity<Usuario>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Email)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.SenhaHash)
                .HasColumnName("SenhaHash")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Papel)
                .HasColumnName("Papel")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.CriadoEm)
                .HasColumnName("CriadoEm");

            modelBuilder.Entity<Usuario>()
                .Property(x => x.AtualizadoEm)
                .HasColumnName("AtualizadoEm");
        }
    }
}

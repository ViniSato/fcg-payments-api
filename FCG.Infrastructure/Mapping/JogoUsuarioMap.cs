using FCG.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Mapping
{
    public static class JogoUsuarioMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JogoUsuario>()
                .ToTable("JogosUsuarios");

            modelBuilder.Entity<JogoUsuario>()
                .HasKey(x => new { x.UsuarioId, x.JogoId });

            modelBuilder.Entity<JogoUsuario>()
                .Property(x => x.UsuarioId)
                .HasColumnName("UsuarioId");

            modelBuilder.Entity<JogoUsuario>()
                .Property(x => x.JogoId)
                .HasColumnName("JogoId");

            modelBuilder.Entity<JogoUsuario>()
                .Property(x => x.DataCompra)
                .HasColumnName("DataCompra");

            modelBuilder.Entity<JogoUsuario>()
                .HasOne(x => x.Usuario)
                .WithMany(u => u.JogosAdquiridos)
                .HasForeignKey(x => x.UsuarioId);

            modelBuilder.Entity<JogoUsuario>()
                .HasOne(x => x.Jogo)
                .WithMany(j => j.UsuariosQueAdquiriram)
                .HasForeignKey(x => x.JogoId);
        }
    }
}

namespace FCG.Domain.Models
{
    public class JogoUsuario
    {
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;

        public int JogoId { get; set; }
        public virtual Jogo Jogo { get; set; } = null!;

        public DateTime DataCompra { get; set; }
    }
}
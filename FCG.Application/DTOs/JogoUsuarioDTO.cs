namespace FCG.Application.DTOs
{
    public class JogoUsuarioDTO
    {
        public int UsuarioId { get; set; }
        public int JogoId { get; set; }
        public DateTime DataCompra { get; set; }

        public string NomeUsuario { get; set; } = string.Empty;
        public string TituloJogo { get; set; } = string.Empty;
    }
}

namespace FCG.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string Papel { get; set; } = "Usuario";
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }

        public List<JogoUsuarioDTO> JogosAdquiridos { get; set; } = new();
    }
}

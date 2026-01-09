namespace FCG.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string Papel { get; set; } = "Usuario"; 
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }

        public virtual ICollection<JogoUsuario> JogosAdquiridos { get; set; } = new List<JogoUsuario>();
    }

}

namespace FCG.Domain.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Genero { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DataLancamento { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }

        public virtual ICollection<JogoUsuario> UsuariosQueAdquiriram { get; set; } = new List<JogoUsuario>();
        public virtual ICollection<Promocao> Promocoes { get; set; } = new List<Promocao>();
    }
}

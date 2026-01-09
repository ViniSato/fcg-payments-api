namespace FCG.Api.Models.Requests
{
    public class JogoRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Genero { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DataLancamento { get; set; }
    }
}

using FCG.Application.DTOs;

namespace FCG.Api.Models.Responses
{
    public class JogoResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Genero { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DataLancamento { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }

        public List<PromocaoDTO> Promocoes { get; set; } = new();
    }
}

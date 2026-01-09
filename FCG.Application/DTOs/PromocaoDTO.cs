namespace FCG.Application.DTOs
{
    public class PromocaoDTO
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public decimal DescontoPercentual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public string TituloJogo { get; set; } = string.Empty;
    }
}

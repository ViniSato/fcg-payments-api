namespace FCG.Api.Models.Responses
{
    public class PromocaoResponse
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public string TituloJogo { get; set; } = string.Empty;
        public decimal DescontoPercentual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}

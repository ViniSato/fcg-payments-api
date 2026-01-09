namespace FCG.Api.Models.Requests
{
    public class PromocaoRequest
    {
        public decimal DescontoPercentual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}

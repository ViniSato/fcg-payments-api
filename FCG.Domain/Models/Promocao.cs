namespace FCG.Domain.Models
{
    public class Promocao
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public virtual Jogo Jogo { get; set; } = null!;

        public decimal DescontoPercentual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}

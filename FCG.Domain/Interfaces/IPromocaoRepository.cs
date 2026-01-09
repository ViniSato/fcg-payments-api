using FCG.Domain.Models;

namespace FCG.Domain.Interfaces
{
    public interface IPromocaoRepository : IBaseRepository<Promocao>
    {
        Task<IEnumerable<Promocao>> GetByJogoId(int jogoId);
    }
}

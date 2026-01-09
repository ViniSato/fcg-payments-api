using FCG.Domain.Models;

namespace FCG.Domain.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario?> GetByEmail(string email);
    }
}

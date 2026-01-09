using FCG.Application.DTOs;

namespace FCG.Application.Interfaces.Services
{
    public interface IPromocaoService
    {
        Task<IEnumerable<PromocaoDTO>> GetByJogoAsync(int jogoId);
        Task<PromocaoDTO> CreateAsync(int jogoId, PromocaoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}

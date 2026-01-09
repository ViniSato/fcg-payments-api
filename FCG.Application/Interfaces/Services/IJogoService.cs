using FCG.Application.DTOs;

namespace FCG.Application.Interfaces
{
    public interface IJogoService
    {
        Task<JogoDTO> GetByIdAsync(int id);
        Task<IEnumerable<JogoDTO>> GetAllAsync();
        Task CreateAsync(JogoDTO dto);
        Task UpdateAsync(JogoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
using FCG.Application.DTOs;

namespace FCG.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> GetUsuarioByIdAsync(int id);
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task CreateUsuarioAsync(UsuarioDTO request);
        Task UpdateUsuarioAsync(UsuarioDTO request);
        Task<bool> DeleteUsuarioAsync(int id);
    }
}

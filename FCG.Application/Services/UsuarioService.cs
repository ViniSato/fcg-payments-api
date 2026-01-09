using FCG.Application.DTOs;
using FCG.Application.Interfaces.Mappers;
using FCG.Application.Interfaces.Services;
using FCG.Application.Interfaces.Services.Auth;
using FCG.Domain.Interfaces;
using FCG.Domain.Models;

namespace FCG.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsuarioMapper _usuarioMapper;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IPasswordHasher passwordHasher,
            IUsuarioMapper usuarioMapper)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
            _usuarioMapper = usuarioMapper;
        }

        public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            return _usuarioMapper.ToDto(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAll();
            return usuarios.Select(_usuarioMapper.ToDto);
        }

        public async Task CreateUsuarioAsync(UsuarioDTO request)
        {
            var senhaHash = _passwordHasher.Hash(request.SenhaHash);

            request.SenhaHash = senhaHash;
            request.CriadoEm = DateTime.Now;

            var usuario = _usuarioMapper.ToEntity(request);

            await _usuarioRepository.Add(usuario);
        }

        public async Task UpdateUsuarioAsync(UsuarioDTO dto)
        {
            var usuario = await _usuarioRepository.GetById(dto.Id);
            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.SenhaHash = _passwordHasher.Hash(dto.SenhaHash);
            usuario.AtualizadoEm = DateTime.Now;

            await _usuarioRepository.Update(usuario);
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            if (usuario == null)
                return false;

            return await _usuarioRepository.Delete(id);
        }
    }
}

using FCG.Application.DTOs;
using FCG.Application.Interfaces;
using FCG.Application.Interfaces.Mappers;
using FCG.Domain.Interfaces;

namespace FCG.Application.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IJogoMapper _jogoMapper;

        public JogoService(IJogoRepository jogoRepository, IJogoMapper jogoMapper)
        {
            _jogoRepository = jogoRepository;
            _jogoMapper = jogoMapper;
        }

        public async Task<JogoDTO> GetByIdAsync(int id)
        {
            var jogo = await _jogoRepository.GetById(id);
            if (jogo == null)
                throw new KeyNotFoundException("Jogo não encontrado");

            return _jogoMapper.ToDto(jogo);
        }

        public async Task<IEnumerable<JogoDTO>> GetAllAsync()
        {
            var jogos = await _jogoRepository.GetAll();
            return jogos.Select(_jogoMapper.ToDto);
        }

        public async Task CreateAsync(JogoDTO dto)
        {
            var jogo = _jogoMapper.ToEntity(dto);
            await _jogoRepository.Add(jogo);
        }

        public async Task UpdateAsync(JogoDTO dto)
        {
            var jogo = await _jogoRepository.GetById(dto.Id);
            if (jogo == null)
                throw new KeyNotFoundException("Jogo não encontrado");

            jogo.Titulo = dto.Titulo;
            jogo.Descricao = dto.Descricao;
            jogo.Genero = dto.Genero;
            jogo.Preco = dto.Preco;
            jogo.DataLancamento = dto.DataLancamento;
            jogo.AtualizadoEm = DateTime.UtcNow;

            await _jogoRepository.Update(jogo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var jogo = await _jogoRepository.GetById(id);
            if (jogo == null)
                return false;

            return await _jogoRepository.Delete(id);
        }
    }
}
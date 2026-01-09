using FCG.Application.DTOs;
using FCG.Application.Interfaces.Services;
using FCG.Domain.Interfaces;
using FCG.Domain.Models;

namespace FCG.Application.Services
{
    public class PromocaoService : IPromocaoService
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IJogoRepository _jogoRepository;

        public PromocaoService(IPromocaoRepository promocaoRepository, IJogoRepository jogoRepository)
        {
            _promocaoRepository = promocaoRepository;
            _jogoRepository = jogoRepository;
        }

        public async Task<IEnumerable<PromocaoDTO>> GetByJogoAsync(int jogoId)
        {
            var promocoes = await _promocaoRepository.GetByJogoId(jogoId);

            return promocoes.Select(p => new PromocaoDTO
            {
                Id = p.Id,
                JogoId = p.JogoId,
                TituloJogo = p.Jogo?.Titulo ?? string.Empty,
                DescontoPercentual = p.DescontoPercentual,
                DataInicio = p.DataInicio,
                DataFim = p.DataFim
            });
        }

        public async Task<PromocaoDTO> CreateAsync(int jogoId, PromocaoDTO dto)
        {
            var jogo = await _jogoRepository.GetById(jogoId);
            if (jogo == null)
                throw new KeyNotFoundException("Jogo não encontrado");

            var promocao = new Promocao
            {
                JogoId = jogoId,
                DescontoPercentual = dto.DescontoPercentual,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim
            };

            await _promocaoRepository.Add(promocao);

            dto.Id = promocao.Id;
            dto.TituloJogo = jogo.Titulo;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _promocaoRepository.Delete(id);
        }
    }
}
using FCG.Application.DTOs;
using FCG.Application.Interfaces.Mappers;
using FCG.Domain.Models;

namespace FCG.Application.Mappers
{
    public class PromocaoMapper : IPromocaoMapper
    {
        public PromocaoDTO ToDto(Promocao entity)
        {
            return new PromocaoDTO
            {
                Id = entity.Id,
                JogoId = entity.JogoId,
                TituloJogo = entity.Jogo?.Titulo ?? string.Empty,
                DescontoPercentual = entity.DescontoPercentual,
                DataInicio = entity.DataInicio,
                DataFim = entity.DataFim
            };
        }

        public Promocao ToEntity(PromocaoDTO dto)
        {
            return new Promocao
            {
                Id = dto.Id,
                JogoId = dto.JogoId,
                DescontoPercentual = dto.DescontoPercentual,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim
            };
        }
    }
}
using FCG.Api.Models.Requests;
using FCG.Api.Models.Responses;
using FCG.Api.Services.Mappers.Interfaces;
using FCG.Application.DTOs;

namespace FCG.Api.Mappers
{
    public class PromocaoApiMapper : IPromocaoMapper
    {
        public PromocaoDTO ToDto(PromocaoRequest request)
        {
            return new PromocaoDTO
            {
                DescontoPercentual = request.DescontoPercentual,
                DataInicio = request.DataInicio,
                DataFim = request.DataFim
            };
        }

        public PromocaoResponse ToResponse(PromocaoDTO dto)
        {
            return new PromocaoResponse
            {
                Id = dto.Id,
                JogoId = dto.JogoId,
                TituloJogo = dto.TituloJogo,
                DescontoPercentual = dto.DescontoPercentual,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim
            };
        }
    }
}
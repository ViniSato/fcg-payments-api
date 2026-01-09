using FCG.Api.Models.Requests;
using FCG.Api.Models.Responses;
using FCG.Api.Services.Mappers.Interfaces;
using FCG.Application.DTOs;

namespace FCG.Api.Services.Mappers
{
    public class JogoMapper : IJogoMapper
    {
        public JogoDTO ToDto(JogoRequest request)
        {
            return new JogoDTO
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Genero = request.Genero,
                Preco = request.Preco,
                DataLancamento = request.DataLancamento,
                CriadoEm = DateTime.UtcNow
            };
        }

        public JogoResponse ToResponse(JogoDTO dto)
        {
            return new JogoResponse
            {
                Id = dto.Id,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Genero = dto.Genero,
                Preco = dto.Preco,
                DataLancamento = dto.DataLancamento,
                CriadoEm = dto.CriadoEm,
                AtualizadoEm = dto.AtualizadoEm,
                Promocoes = dto.Promocoes.Select(p => new PromocaoDTO
                {
                    Id = p.Id,
                    JogoId = p.JogoId,
                    TituloJogo = p.TituloJogo,
                    DescontoPercentual = p.DescontoPercentual,
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim
                }).ToList()
            };
        }
    }
}
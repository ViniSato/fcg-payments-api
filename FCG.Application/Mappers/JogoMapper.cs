using FCG.Application.DTOs;
using FCG.Application.Interfaces.Mappers;
using FCG.Domain.Models;

namespace FCG.Application.Mappers
{
    public class JogoMapper : IJogoMapper
    {
        public Jogo ToEntity(JogoDTO dto)
        {
            return new Jogo
            {
                Id = dto.Id,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Genero = dto.Genero,
                Preco = dto.Preco,
                DataLancamento = dto.DataLancamento,
                CriadoEm = dto.CriadoEm,
                AtualizadoEm = dto.AtualizadoEm,
                Promocoes = dto.Promocoes.Select(p => new Promocao
                {
                    Id = p.Id,
                    JogoId = p.JogoId,
                    DescontoPercentual = p.DescontoPercentual,
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim
                }).ToList()
            };
        }

        public JogoDTO ToDto(Jogo entity)
        {
            return new JogoDTO
            {
                Id = entity.Id,
                Titulo = entity.Titulo,
                Descricao = entity.Descricao,
                Genero = entity.Genero,
                Preco = entity.Preco,
                DataLancamento = entity.DataLancamento,
                CriadoEm = entity.CriadoEm,
                AtualizadoEm = entity.AtualizadoEm,
                Promocoes = entity.Promocoes.Select(p => new PromocaoDTO
                {
                    Id = p.Id,
                    JogoId = p.JogoId,
                    TituloJogo = entity.Titulo,
                    DescontoPercentual = p.DescontoPercentual,
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim
                }).ToList()
            };
        }
    }
}
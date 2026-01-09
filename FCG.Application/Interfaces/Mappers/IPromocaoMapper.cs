using FCG.Application.DTOs;
using FCG.Domain.Models;

namespace FCG.Application.Interfaces.Mappers
{
    public interface IPromocaoMapper
    {
        PromocaoDTO ToDto(Promocao entity);
        Promocao ToEntity(PromocaoDTO dto);
    }
}

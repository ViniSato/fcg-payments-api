using FCG.Api.Models.Requests;
using FCG.Api.Models.Responses;
using FCG.Application.DTOs;

namespace FCG.Api.Services.Mappers.Interfaces
{
    public interface IPromocaoMapper
    {
        PromocaoDTO ToDto(PromocaoRequest request);
        PromocaoResponse ToResponse(PromocaoDTO dto);
    }
}

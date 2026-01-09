using FCG.Api.Models.Requests;
using FCG.Api.Models.Responses;
using FCG.Application.DTOs;
using FCG.Domain.Models;

namespace FCG.Api.Services.Mappers.Interfaces
{
    public interface IUsuarioMapper
    {
        UsuarioDTO ToDto(UsuarioRequest request);
        UsuarioResponse ToResponse(UsuarioDTO dto);
    }
}
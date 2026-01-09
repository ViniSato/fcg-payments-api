using FCG.Api.Models.Requests;
using FCG.Api.Models.Responses;
using FCG.Api.Services.Mappers.Interfaces;
using FCG.Application.DTOs;

namespace FCG.Api.Services.Mappers
{
    public class UsuarioMapper : IUsuarioMapper
    {
        public UsuarioDTO ToDto(UsuarioRequest request)
        {
            return new UsuarioDTO
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = request.Senha,
                Papel = request.Papel,
                CriadoEm = DateTime.UtcNow
            };
        }

        public UsuarioResponse ToResponse(UsuarioDTO dto)
        {
            return new UsuarioResponse
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Email = dto.Email,
                Papel = dto.Papel,
                CriadoEm = dto.CriadoEm,
                AtualizadoEm = dto.AtualizadoEm
            };
        }
    }

}

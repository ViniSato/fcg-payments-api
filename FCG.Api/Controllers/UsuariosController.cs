using FCG.Api.Models.Requests;
using FCG.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FCG.Application.Interfaces.Services;

namespace FCG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly Services.Mappers.Interfaces.IUsuarioMapper _usuarioMapper;

        public UsuariosController(
            IUsuarioService usuarioService,
            Services.Mappers.Interfaces.IUsuarioMapper usuarioMapper)
        {
            _usuarioService = usuarioService;
            _usuarioMapper = usuarioMapper;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id)
                ?? throw new NotFoundException("Usuário não encontrado");

            var response = _usuarioMapper.ToResponse(usuario);
            return Ok(response);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _usuarioService.GetAllAsync();
            var responses = dtos.Select(_usuarioMapper.ToResponse);
            return Ok(responses);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioRequest request)
        {
            if (request == null)
                throw new ValidationException(new[] { "Requisição inválida." });

            var dto = _usuarioMapper.ToDto(request);
            await _usuarioService.CreateUsuarioAsync(dto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioRequest request)
        {
            if (request == null)
                throw new ValidationException(new[] { "Requisição inválida." });

            var usuarioExistente = await _usuarioService.GetUsuarioByIdAsync(id)
                ?? throw new NotFoundException("Usuário não encontrado");

            var dto = _usuarioMapper.ToDto(request);
            dto.Id = id;

            await _usuarioService.UpdateUsuarioAsync(dto);
            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var sucesso = await _usuarioService.DeleteUsuarioAsync(id);
            if (!sucesso)
                throw new NotFoundException("Usuário não encontrado");

            return NoContent();
        }
    }
}
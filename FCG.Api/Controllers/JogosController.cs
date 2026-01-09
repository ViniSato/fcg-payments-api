using FCG.Api.Models.Requests;
using FCG.Application.Interfaces;
using FCG.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IJogoMapper = FCG.Api.Services.Mappers.Interfaces.IJogoMapper;

namespace FCG.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;
        private readonly IJogoMapper _jogoMapper;

        public JogosController(IJogoService jogoService, IJogoMapper jogoMapper)
        {
            _jogoService = jogoService;
            _jogoMapper = jogoMapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _jogoService.GetByIdAsync(id)
                ?? throw new NotFoundException("Jogo não encontrado");

            var response = _jogoMapper.ToResponse(dto);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("biblioteca")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _jogoService.GetAllAsync();
            var responses = dtos.Select(_jogoMapper.ToResponse);
            return Ok(responses);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Create([FromBody] JogoRequest request)
        {
            if (request == null)
                throw new ValidationException(new[] { "Requisição inválida." });

            var dto = _jogoMapper.ToDto(request);
            await _jogoService.CreateAsync(dto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JogoRequest request)
        {
            if (request == null)
                throw new ValidationException(new[] { "Requisição inválida." });

            var dto = _jogoMapper.ToDto(request);
            dto.Id = id;
            await _jogoService.UpdateAsync(dto);
            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _jogoService.DeleteAsync(id);
            if (!sucesso)
                throw new NotFoundException("Jogo não encontrado");

            return NoContent();
        }
    }
}
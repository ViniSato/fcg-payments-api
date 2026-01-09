using FCG.Api.Models.Requests;
using FCG.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FCG.Application.Interfaces.Services.Auth;

namespace FCG.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AutenticacaoController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null)
                throw new ValidationException(new[] { "Requisição inválida." });

            var token = await _authService.AutenticarAsync(
                request.ToEmail(),
                request.ToSenha()
            ) ?? throw new UnauthorizedException("Credenciais inválidas.");

            return Ok(new { Token = token });
        }
    }
}
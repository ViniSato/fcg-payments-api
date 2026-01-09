using FCG.Application.Interfaces.Services.Auth;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;

namespace FCG.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> AutenticarAsync(Email email, Senha senha)
        {
            var usuario = await _usuarioRepository.GetByEmail(email.Endereco);
            if (usuario == null)
                throw new UnauthorizedAccessException("Credenciais inválidas");

            var senhaValida = _passwordHasher.Verify(senha.Valor, usuario.SenhaHash);
            if (!senhaValida)
                throw new UnauthorizedAccessException("Credenciais inválidas");

            var token = _tokenGenerator.GerarToken(usuario);
            return token;
        }
    }
}
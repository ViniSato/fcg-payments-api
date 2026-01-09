using FCG.Domain.ValueObjects;

namespace FCG.Api.Models.Requests
{
    public class UsuarioRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Papel { get; set; } = "Usuario";
    }
}

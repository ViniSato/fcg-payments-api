using FCG.Domain.ValueObjects;

namespace FCG.Application.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<string> AutenticarAsync(Email email, Senha senha);
    }
}
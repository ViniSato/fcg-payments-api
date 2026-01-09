using FCG.Domain.Models;

namespace FCG.Application.Interfaces.Services.Auth
{
    public interface ITokenGenerator
    {
        string GerarToken(Usuario usuario);
    }
}
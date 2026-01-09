using FCG.Application.Interfaces;
using FCG.Application.Interfaces.Mappers;
using FCG.Application.Interfaces.Services;
using FCG.Application.Interfaces.Services.Auth;
using FCG.Application.Mappers;
using FCG.Application.Services;
using FCG.Application.Services.Auth;

namespace FCG.Api.IoC.Modules
{
    public class ServiceModule
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IJogoService, JogoService>();
            services.AddTransient<IPromocaoService, PromocaoService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IUsuarioMapper, UsuarioMapper>();
            services.AddTransient<IJogoMapper, JogoMapper>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
        }
    }
}

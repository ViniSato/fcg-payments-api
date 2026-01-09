using FCG.Api.Services.Mappers;
using FCG.Api.Services.Mappers.Interfaces;

namespace FCG.Api.IoC.Modules
{
    public class ApiModules
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<IUsuarioMapper, UsuarioMapper>();
            services.AddTransient<IJogoMapper, JogoMapper>();
        }
    }
}

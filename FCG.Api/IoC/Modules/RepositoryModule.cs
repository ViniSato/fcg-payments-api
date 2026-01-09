using FCG.Domain.Interfaces;
using FCG.Infrastructure.Repositories;

namespace FCG.Api.IoC.Modules
{
    public class RepositoryModule
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IJogoRepository, JogoRepository>();
            services.AddTransient<IPromocaoRepository, PromocaoRepository>();
            services.AddTransient<ILogRepository, MongoLogRepository>();
        }
    }
}

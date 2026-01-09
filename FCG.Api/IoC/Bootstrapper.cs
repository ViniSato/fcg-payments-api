using FCG.Api.IoC.Modules;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FCG.Api.IoC
{
    public static class Bootstrapper
    {
        public static IServiceCollection StartRegisterServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            ApiModules.InjectDependencies(services);
            RepositoryModule.InjectDependencies(services);
            ServiceModule.InjectDependencies(services);
            return services;
        }
    }
}

using FCG.Application.Interfaces.Services;
using FCG.Application.Services;

namespace FCG.Api.IoC.Modules
{
    public class ServiceModule
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ILogService, LogService>();
        }
    }
}

using FCG.Domain.Interfaces;
using FCG.Infrastructure.Repositories;

namespace FCG.Api.IoC.Modules
{
    public class RepositoryModule
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ILogRepository, MongoLogRepository>();
        }
    }
}

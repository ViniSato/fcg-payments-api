using FCG.Domain.Models;
using FCG.Infrastructure.Context;
using Microsoft.Extensions.Logging;

namespace FCG.Infrastructure.Repositories
{
    public class JogoRepository : BaseRepository<Jogo>, Domain.Interfaces.IJogoRepository
    {
        public JogoRepository(FCGContext context, ILogger<JogoRepository> logger) : base(context, logger)
        {

        }
    }
}

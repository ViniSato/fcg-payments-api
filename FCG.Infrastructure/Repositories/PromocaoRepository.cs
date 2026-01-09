using FCG.Domain.Models;
using FCG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FCG.Infrastructure.Repositories
{
    public class PromocaoRepository : BaseRepository<Promocao>, Domain.Interfaces.IPromocaoRepository
    {
        private readonly FCGContext _context;

        public PromocaoRepository(FCGContext context, ILogger<PromocaoRepository> logger)
            : base(context, logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promocao>> GetByJogoId(int jogoId)
        {
            return await _context.Set<Promocao>()
                .AsNoTracking()
                .Where(j => j.JogoId == jogoId)
                .Include(p => p.Jogo) 
                .ToListAsync();
        }
    }
}

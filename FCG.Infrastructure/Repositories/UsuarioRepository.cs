using FCG.Domain.Interfaces;
using FCG.Domain.Models;
using FCG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FCG.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly FCGContext _context;

        public UsuarioRepository(FCGContext context, ILogger<UsuarioRepository> logger)
            : base(context, logger)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            return await _context.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
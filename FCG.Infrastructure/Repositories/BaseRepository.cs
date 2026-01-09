using FCG.Domain.Interfaces;
using FCG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FCG.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly FCGContext _context;
        protected readonly ILogger<BaseRepository<TEntity>> _logger;

        public BaseRepository(FCGContext context, ILogger<BaseRepository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Delete failed: {Entity} with ID {Id} not found.", typeof(TEntity).Name, id);
                return false;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted {Entity} with ID {Id}.", typeof(TEntity).Name, id);
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("GetById failed: {Entity} with ID {Id} not found.", typeof(TEntity).Name, id);
                throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with ID {id} not found.");
            }

            return entity;
        }

        public async Task Add(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added new {Entity}.", typeof(TEntity).Name);
        }

        public async Task<TEntity> AddEntity(TEntity obj)
        {
            var entity = _context.Set<TEntity>().Add(obj).Entity;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added and returned new {Entity}.", typeof(TEntity).Name);
            return entity;
        }

        public async Task AddRange(IEnumerable<TEntity> objs)
        {
            _context.Set<TEntity>().AddRange(objs);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added range of {Entity}.", typeof(TEntity).Name);
        }

        public async Task Update(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated {Entity}.", typeof(TEntity).Name);
        }

        public async Task UpdateRange(IEnumerable<TEntity> objs)
        {
            _context.Set<TEntity>().UpdateRange(objs);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated range of {Entity}.", typeof(TEntity).Name);
        }
    }
}
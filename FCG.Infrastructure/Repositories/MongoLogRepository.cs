using FCG.Domain.Interfaces;
using FCG.Domain.Models;
using MongoDB.Driver;

namespace FCG.Infrastructure.Repositories
{
    public class MongoLogRepository : ILogRepository
    {
        private readonly IMongoCollection<LogEntry> _collection;

        public MongoLogRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<LogEntry>("Logs");
        }

        public async Task SaveAsync(LogEntry entry)
        {
            await _collection.InsertOneAsync(entry);
        }
    }
}

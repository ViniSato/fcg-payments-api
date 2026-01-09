using FCG.Application.Interfaces.Services;
using FCG.Domain.Interfaces;
using FCG.Domain.Models;

namespace FCG.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _repository;

        public LogService(ILogRepository repository)
        {
            _repository = repository;
        }

        public async Task SaveAsync(LogEntry entry)
        {
            await _repository.SaveAsync(entry);
        }
    }

}

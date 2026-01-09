using FCG.Domain.Models;

namespace FCG.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task SaveAsync(LogEntry entry);
    }
}

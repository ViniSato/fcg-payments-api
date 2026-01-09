using FCG.Domain.Models;

namespace FCG.Application.Interfaces.Services
{
    public interface ILogService
    {
        Task SaveAsync(LogEntry entry);
    }
}

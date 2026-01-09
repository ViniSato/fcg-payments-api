namespace FCG.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T obj);
        Task<T> AddEntity(T obj);
        Task AddRange(IEnumerable<T> objs);
        Task Update(T obj);
        Task<bool> Delete(int id);
        Task UpdateRange(IEnumerable<T> objs);
    }
}

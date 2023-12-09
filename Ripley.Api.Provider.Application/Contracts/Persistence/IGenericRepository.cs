namespace Ripley.Api.Provider.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(long id);
        Task<T> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        T Update(T entity);
        Task<bool> DeleteAsync(int id);
    }
}

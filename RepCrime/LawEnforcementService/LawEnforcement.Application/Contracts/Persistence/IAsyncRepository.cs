using System.Linq.Expressions;

namespace LawEnforcement.Application.Contracts
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task Save();
    }
}

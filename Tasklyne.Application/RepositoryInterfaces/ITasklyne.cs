using System.Linq.Expressions;

namespace Tasklyne.Application.RepositoryInterfaces;

public interface ITasklyne<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T,bool>> filter, string? includeProperties = null);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    
}
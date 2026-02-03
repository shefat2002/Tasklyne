using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tasklyne.Application.Common.Interfaces;
using Tasklyne.Infrastructure.Data;

namespace Tasklyne.Infrastructure.Repositories;

public class TasklyneRespository<T> : ITasklyne<T> where T :class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;


    public TasklyneRespository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (filter != null) query = query.Where(filter);

        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split())
            {
                
            }
        }
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }
}
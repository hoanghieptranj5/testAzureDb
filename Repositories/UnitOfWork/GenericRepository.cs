using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Model;

namespace Repositories.UnitOfWork;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected ApplicationDbContext Context;
    private DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<T>();
    }

    public async Task<IEnumerable<T>> All()
    {
        return await _dbSet.Take(10).ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> Add(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
        }
        catch (Exception e)
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _dbSet.Remove(entity);
        }
        catch (Exception e)
        {
            return false;
        }
        
        return true;
    }

    public Task<bool> Upsert(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet
            .Where(predicate)
            .ToListAsync();
    }
}
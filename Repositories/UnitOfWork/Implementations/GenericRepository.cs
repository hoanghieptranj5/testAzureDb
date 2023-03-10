using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;

namespace Repositories.UnitOfWork.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected ApplicationDbContext Context;
    private DbSet<T> _dbSet;
    private ILogger _logger;

    public GenericRepository(ApplicationDbContext context, ILogger logger)
    {
        Context = context;
        _logger = logger;
        _dbSet = Context.Set<T>();
        
    }

    public async Task<IEnumerable<T>> All()
    {
        return await _dbSet.ToListAsync();
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
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new Exception($"{id.ToString()} not found!");
        }
        _dbSet.Remove(entity);

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
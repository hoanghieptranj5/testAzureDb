using Microsoft.Extensions.Logging;
using Repositories.Model;

namespace Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    
    public IAddressRepository Addresses { get; set; }

    public UnitOfWork(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
    {
        _dbContext = dbContext;
        Addresses = new AddressRepository(_dbContext, loggerFactory.CreateLogger("Hello"));
    }

    public async Task CompleteAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
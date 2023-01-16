using Microsoft.Extensions.Logging;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;

namespace Repositories.UnitOfWork.Implementations;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    
    public IAddressRepository Addresses { get; set; }
    public IProductRepository Products { get; set; }
    public IElectricPriceRepository ElectricPriceRepository { get; set; }

    public UnitOfWork(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
    {
        _dbContext = dbContext;
        
        var logger = loggerFactory.CreateLogger<UnitOfWork>();
        
        Addresses = new AddressRepository(_dbContext, logger);
        Products = new ProductRepository(_dbContext, logger);
        ElectricPriceRepository = new ElectricPriceRepository(_dbContext, logger);
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
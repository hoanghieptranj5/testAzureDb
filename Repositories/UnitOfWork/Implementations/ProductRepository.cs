using Microsoft.Extensions.Logging;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;

namespace Repositories.UnitOfWork.Implementations;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
using Microsoft.Extensions.Logging;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;

namespace Repositories.UnitOfWork.Implementations;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    private ILogger _logger;
    
    public AddressRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
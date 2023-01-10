using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Repositories.Model;

namespace Repositories.UnitOfWork;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    private ILogger _logger;
    
    public AddressRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
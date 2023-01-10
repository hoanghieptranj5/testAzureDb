using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Repositories.Model;

namespace Repositories.UnitOfWork;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }
}
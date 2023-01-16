using Microsoft.Extensions.Logging;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;

namespace Repositories.UnitOfWork.Implementations;

public class ElectricPriceRepository : GenericRepository<ElectricPrice>, IElectricPriceRepository
{
    public ElectricPriceRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
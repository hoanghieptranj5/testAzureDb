using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Model;

namespace TestAzureDb.Services.Abstractions;

public interface IElectricPriceService
{
    Task<ElectricPrice> GetSingleById(int id);
    Task<IEnumerable<ElectricPrice>> GetList();
}
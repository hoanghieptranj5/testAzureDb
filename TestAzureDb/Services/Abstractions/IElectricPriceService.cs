using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Model;
using TestAzureDb.Models;

namespace TestAzureDb.Services.Abstractions;

public interface IElectricPriceService
{
    Task<ElectricPrice> GetSingleById(int id);
    Task<IEnumerable<ElectricPrice>> GetList();
    Task<bool> RemoveSingle(string id);
    Task<bool> CreateSingle(CreateElectricPriceRequestModel requestModel);
}
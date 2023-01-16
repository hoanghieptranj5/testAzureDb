using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;
using TestAzureDb.Services.Abstractions;

namespace TestAzureDb.Services;

public class ElectricPriceService : IElectricPriceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public ElectricPriceService(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ElectricPrice> GetSingleById(int id)
    {
        var item = await _unitOfWork.ElectricPriceRepository.GetById(ToGuid(id));
        return item;
    }

    public async Task<IEnumerable<ElectricPrice>> GetList()
    {
        var items = await _unitOfWork.ElectricPriceRepository.All();
        return items;
    }
    
    private static Guid ToGuid(int value)
    {
        var bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }
}
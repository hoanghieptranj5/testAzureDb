using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Converters;
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
        var item = await _unitOfWork.ElectricPrices.GetById(IntegerConvert.ToGuid(id));
        return item;
    }

    public async Task<IEnumerable<ElectricPrice>> GetList()
    {
        var items = await _unitOfWork.ElectricPrices.All();
        return items;
    }
}
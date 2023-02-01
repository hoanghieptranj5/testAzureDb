using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.Converters;
using Microsoft.Extensions.Logging;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;
using TestAzureDb.Models;
using TestAzureDb.Services.Abstractions;

namespace TestAzureDb.Services;

public class ElectricPriceService : IElectricPriceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public ElectricPriceService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

    public async Task<bool> RemoveSingle(string id)
    {
        var deleted = await _unitOfWork.ElectricPrices.Delete(StringConvert.ToGuid(id));
        await _unitOfWork.CompleteAsync();
        
        return deleted;
    }

    public async Task<bool> CreateSingle(CreateElectricPriceRequestModel requestModel)
    {
        var result = await _unitOfWork.ElectricPrices.Add(_mapper.Map<ElectricPrice>(requestModel));
        await _unitOfWork.CompleteAsync();
        return result;
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;
using TestAzureDb.Models;
using TestAzureDb.Services.Abstractions;

namespace TestAzureDb.Functions;

public class ElectricFunctions
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElectricPriceService _electricPriceService;
    private readonly IMapper _mapper;

    public ElectricFunctions(IUnitOfWork unitOfWork, IElectricPriceService electricPriceService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _electricPriceService = electricPriceService;
        _mapper = mapper;
    }

    [ApiExplorerSettings(GroupName = "ElectricPricesApi")]
    [FunctionName("GetElectricPrices")]
    public async Task<IActionResult> GetElectricPrices(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "electricPrices")] HttpRequest req,
        ILogger log)
    {
        var result = await _unitOfWork.ElectricPrices.All();
        return new OkObjectResult(result);
    }

    [ApiExplorerSettings(GroupName = "ElectricPricesApi")]
    [FunctionName("AddElectricPrices")]
    public async Task<IActionResult> AddElectricPrice(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "electricPrices/{priceId}")]
        [RequestBodyType(typeof(CreateElectricPriceRequestModel), "Create type")]
        HttpRequest req,
        ILogger log)
    {
        var requestObject = await new StreamReader(req.Body).ReadToEndAsync();
        var requestModel = JsonConvert.DeserializeObject<CreateElectricPriceRequestModel>(requestObject);
        var result = await _electricPriceService.CreateSingle(requestModel);

        return new OkObjectResult(result);
    }

    [ApiExplorerSettings(GroupName = "ElectricPricesApi")]
    [FunctionName("RemoveElectricPrice")]
    public async Task<IActionResult> RemoveElectricPrice(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "electricPrices/{priceId}")]
        HttpRequest req,
        string priceId,
        ILogger log)
    {
        bool result;
        try
        {
            result = await _electricPriceService.RemoveSingle(priceId);
        }
        catch (Exception ex)
        {
            return new OkObjectResult(ex.Message);
        }

        return new OkObjectResult(result);
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Common.Converters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositories.UnitOfWork.Abstractions;
using TestAzureDb.Models;
using TestAzureDb.Services.Abstractions;

namespace TestAzureDb.Functions;

public class ElectricFunctions
{
    #region Constants

    private const string ElectricApi = "ElectricPricesApi";

    #endregion
    
    private readonly IElectricPriceService _electricPriceService;

    public ElectricFunctions(IElectricPriceService electricPriceService)
    {
        _electricPriceService = electricPriceService;
    }

    [ApiExplorerSettings(GroupName = ElectricApi)]
    [FunctionName("GetElectricPrices")]
    public async Task<IActionResult> GetElectricPrices(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "electricPrices")] HttpRequest req,
        ILogger log)
    {
        var result = await _electricPriceService.GetList();
        return new OkObjectResult(result);
    }

    [ApiExplorerSettings(GroupName = ElectricApi)]
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

    [ApiExplorerSettings(GroupName = ElectricApi)]
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
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;
using TestAzureDb.Models;

namespace TestAzureDb.Functions;

public class ElectricFunctions
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ElectricFunctions(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [FunctionName("GetElectricPrices")]
    public async Task<IActionResult> GetElectricPrices(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "electricPrices")] HttpRequest req,
        ILogger log)
    {
        var result = await _unitOfWork.ElectricPriceRepository.All();
        return new OkObjectResult(result);
    }

    [FunctionName("AddElectricPrices")]
    public async Task<IActionResult> AddElectricPrice(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "electricPrices/{priceId}")]
        [RequestBodyType(typeof(CreateElectricPriceRequestModel), "Create type")]
        HttpRequest req,
        ILogger log)
    {
        var requestObject = await new StreamReader(req.Body).ReadToEndAsync();
        var requestModel = JsonConvert.DeserializeObject<CreateElectricPriceRequestModel>(requestObject);
        var result = await _unitOfWork.ElectricPriceRepository.Add(_mapper.Map<ElectricPrice>(requestModel));
        await _unitOfWork.CompleteAsync();

        return new OkObjectResult(result);
    }
}
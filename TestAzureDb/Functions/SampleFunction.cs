using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositories.UnitOfWork;

namespace TestAzureDb.Functions;

public class SampleFunction
{
    private readonly IUnitOfWork _unitOfWork;

    public SampleFunction(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [FunctionName("SampleOne")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, 
        ILogger log) 
    {
        log.LogInformation("C# Http trigger function processed a request.");

        string name = req.Query["name"];
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        name ??= data?.name;

        string responseMessage = string.IsNullOrEmpty(name)
            ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {name}. This HTTP triggered function executed successfully.";

        return new OkObjectResult(responseMessage);
    }
    
    [FunctionName("SampleTwo")]
    public async Task<IActionResult> Test(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, 
        ILogger log)
    {
        var result = await _unitOfWork.Addresses.All();
        return new OkObjectResult(result);
    }
}
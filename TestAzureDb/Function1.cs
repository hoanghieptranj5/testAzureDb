using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TestAzureDb;
using TestAzureDb.Logic;
using TestAzureDb.Model;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TestAzureDb;

public class Function1
{
    private readonly MyTimerFunction _myTimerFunction;

    public Function1(MyTimerFunction myTimerFunction)
    {
        _myTimerFunction = myTimerFunction;
    }

    [FunctionName("Function1")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
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
    
    [FunctionName("GetAllAddresses")]
    [ProducesResponseType(typeof(IEnumerable<Address>), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllAddresses(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] [RequestBodyType(typeof(Address), "Desc")] HttpRequest req,
        ILogger log)
    {
        var result = await _myTimerFunction.GetAddresses();
        return new OkObjectResult(result);
    }
}
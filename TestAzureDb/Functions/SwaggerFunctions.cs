using System.Net.Http;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using TestAzureDb;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TestAzureDb.Functions;

public static class SwaggerFunctions
{
    [SwaggerIgnore]
    [FunctionName("Swagger")]
    public static Task<HttpResponseMessage> Swagger(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/json")] HttpRequestMessage req,
        [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
    {
        return Task.FromResult(swashBuckleClient.CreateSwaggerJsonDocumentResponse(req));
    }
    
    [SwaggerIgnore]
    [FunctionName("SwaggerUI")]
    public static Task<HttpResponseMessage> SwaggerUI(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequestMessage req,
        [SwashBuckleClient] ISwashBuckleClient swashBuckleClient) 
    {
        return Task.FromResult(swashBuckleClient.CreateSwaggerUIResponse(req, "swagger/json"));
    }
}
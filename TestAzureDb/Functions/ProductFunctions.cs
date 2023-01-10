using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Repositories.UnitOfWork.Abstractions;

namespace TestAzureDb.Functions;

public class ProductFunctions
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ProductFunctions(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [FunctionName("FindProductById")]
    public async Task<IActionResult> FindProductById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "product/{productId}")] HttpRequest req,
        int productId,
        ILogger log)
    {
        var result = await _unitOfWork.Products.Find(x => x.ProductId == productId);
        return new OkObjectResult(result);
    }
}
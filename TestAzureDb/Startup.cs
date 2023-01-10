using System;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Model;
using Swashbuckle.AspNetCore.SwaggerGen;
using TestAzureDb.Logic;

namespace TestAzureDb;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts =>
        {
            opts.AddCodeParameter = true;
            opts.Documents = new[]
            {
                new SwaggerDocument()
                {
                    Name = "v1",
                    Title = "Swagger document",
                    Description = "Integrate Swagger UI With Azure Functions",
                    Version = "v2"
                }
            };

            opts.ConfigureSwaggerGen = x =>
            {
                x.CustomOperationIds(apiDesc => 
                    apiDesc.TryGetMethodInfo(out var mInfo) ? mInfo.Name : default(Guid).ToString());
            };
        });
        
        
        string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

        builder.Services.AddScoped<MyTimerFunction>();
    }
}
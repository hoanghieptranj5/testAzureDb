using System;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Model;
using Repositories.UnitOfWork.Abstractions;
using Repositories.UnitOfWork.Implementations;
using Swashbuckle.AspNetCore.SwaggerGen;
using TestAzureDb.Profiles;
using TestAzureDb.Services;
using TestAzureDb.Services.Abstractions;

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

        builder.Services.AddAutoMapper(typeof(ElectricPriceProfile));

        builder.Services.AddLogging();

        string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString", EnvironmentVariableTarget.Process);
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IElectricPriceService, ElectricPriceService>();
    }
}
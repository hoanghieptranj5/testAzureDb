using AutoMapper;
using Repositories.Model;
using TestAzureDb.Models;

namespace TestAzureDb.Profiles;

public class ElectricPriceProfile : Profile
{
    public ElectricPriceProfile()
    {
        CreateMap<CreateElectricPriceRequestModel, ElectricPrice>();
    }
}
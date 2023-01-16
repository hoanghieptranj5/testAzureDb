namespace Repositories.UnitOfWork.Abstractions;

public interface IUnitOfWork
{
    IAddressRepository Addresses { get; set; }
    IProductRepository Products { get; set; }
    IElectricPriceRepository ElectricPriceRepository { get; set; }

    Task CompleteAsync();
}
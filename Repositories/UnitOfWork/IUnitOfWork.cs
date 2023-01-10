namespace Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IAddressRepository Addresses { get; set; }

    Task CompleteAsync();
}
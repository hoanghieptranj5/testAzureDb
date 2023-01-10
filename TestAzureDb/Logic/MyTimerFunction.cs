using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Model;

namespace TestAzureDb.Logic;

public class MyTimerFunction
{
    private readonly ApplicationDbContext _dbContext;

    public MyTimerFunction(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Address>> GetAddresses()
    {
        var addresses = await _dbContext.Addresses.Take(1).ToListAsync();
        return addresses;
    }
}
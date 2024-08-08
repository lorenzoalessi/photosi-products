using Bogus;
using Microsoft.EntityFrameworkCore;
using PhotosiProducts.Model;

namespace PhotosiProducts.UnitTest;

// https://learn.microsoft.com/it-it/dotnet/core/testing/unit-testing-with-nunit
public abstract class TestSetup
{
    protected Context _context;

    protected Randomizer _faker;

    protected virtual void SetUp()
    {
        _faker = new Randomizer();

        var dbOptions = new DbContextOptionsBuilder<Context>()
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new Context(dbOptions);
    }
}
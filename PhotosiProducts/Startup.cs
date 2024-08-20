using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PhotosiProducts.Model;
using PhotosiProducts.Repository.Category;
using PhotosiProducts.Repository.Product;
using PhotosiProducts.Service;

namespace PhotosiProducts;

[ExcludeFromCodeCoverage]
public class Startup
{
    private readonly WebApplicationBuilder _builder;

    public Startup(WebApplicationBuilder builder)
    {
        _builder = builder;
    }

    public async Task ConfigureServices()
    {
        _ = _builder.Services.AddControllers();

        _builder.Services.AddAutoMapper(typeof(Startup));
        ConfigureMyServices(_builder.Services);
        ConfigureRepositories(_builder.Services);

        await ConfigureDb();
    }
    
    public void Configure(IApplicationBuilder app)
    {
        _ = app.UseRouting();
        _ = app.UseAuthorization();
        _ = app.UseEndpoints(x => x.MapControllers());
    }

    private async Task ConfigureDb()
    {
        _ = _builder.Services.AddDbContext<Context>(options =>
            options.UseNpgsql(_builder.Configuration.GetConnectionString("PostgreSql"))
        );

        await using var serviceProvider = _builder.Services.BuildServiceProvider();
        // Applico le migrazioni
        await serviceProvider.GetRequiredService<Context>().Database.MigrateAsync();
        
        // Seeder per dei dati iniziali
        await serviceProvider.GetRequiredService<Seeder.Seeder>().SeedDb();
    }

    private void ConfigureMyServices(IServiceCollection services)
    {
        // Aggiunge i propri servizi al container di dependency injection.
        _ = services.AddScoped<IProductService, ProductService>()
                .AddScoped<Seeder.Seeder>()
            ;
    }

    private void ConfigureRepositories(IServiceCollection services)
    {
        // Aggiunge i repository al container di dependency injection.
        _ = services.AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
            ;
    }
}
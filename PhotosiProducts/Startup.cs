using Microsoft.EntityFrameworkCore;
using PhotosiProducts.Model;
using PhotosiProducts.Repository.Products;
using PhotosiProducts.Services;

namespace PhotosiProducts;

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

        ConfigureMyServices(_builder.Services);

        ConfigureRepositories(_builder.Services);

        await ConfigureDb();
    }
    
    /// <summary>
    /// Configura l'applicazione.
    /// </summary>
    /// <param name="app">L'oggetto IApplicationBuilder.</param>
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
    }

    private void ConfigureMyServices(IServiceCollection services)
    {
        // Aggiunge i propri servizi al container di dependency injection.
        _ = services
            // .AddScoped<IMigratorSeeder, MigratorSeeder>()
            .AddScoped<IProductsService, ProductsService>()
            ;
    }

    private void ConfigureRepositories(IServiceCollection services)
    {
        // Aggiunge i repository al container di dependency injection.
        _ = services
            .AddScoped<IProductsRepository, ProductsRepository>()
            // .AddScoped<ICategoryRepository, CategoryRepository>()
            ;
    }
}
using Microsoft.EntityFrameworkCore;
using PhotosiProducts.Model;

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

        _builder.Services.AddAutoMapper(typeof(Startup));

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
        // TODO: Dati di prova all'avvio
        // IMigratorSeeder migratorSeeder = serviceProvider.GetRequiredService<IMigratorSeeder>();
        // await migratorSeeder.ApplyMigration();
        // await migratorSeeder.SeedDb();
    }

    private void ConfigureMyServices(IServiceCollection services)
    {
        // Aggiunge i propri servizi al container di dependency injection.
        _ = services
            // .AddScoped<IMigratorSeeder, MigratorSeeder>()
            // .AddScoped<IProductService, ProductService>()
            ;
    }

    private void ConfigureRepositories(IServiceCollection services)
    {
        // Aggiunge i repository al container di dependency injection.
        _ = services
            // .AddScoped<IProductRepository, ProductRepository>()
            // .AddScoped<ICategoryRepository, CategoryRepository>()
            ;
    }
}
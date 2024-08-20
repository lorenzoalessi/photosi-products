using System.Diagnostics.CodeAnalysis;
using PhotosiProducts.Model;

namespace PhotosiProducts.Seeder;

[ExcludeFromCodeCoverage]
public class Seeder
{
    private readonly Context _context;

    public Seeder(Context context)
    {
        _context = context;
    }

    public async Task SeedDb()
    {
        if (!_context.Categories.Any())
        {
            var products = new List<Product>();

            for (int i = 0; i < 10; i++)
            {
                var category = new Category
                {
                    Name = $"Categoria {i}"
                };

                products.Add(new Product
                {
                    Name = $"Prodotto {i}",
                    Description = $"Prodotto di prova",
                    Category = category
                });
            }

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
    }
}
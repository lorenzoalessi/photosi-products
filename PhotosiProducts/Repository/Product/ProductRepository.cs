using System.Diagnostics.CodeAnalysis;
using PhotosiProducts.Model;

namespace PhotosiProducts.Repository.Product;

// TODO Escluso dai test fino a che non implementa metodi custom 
[ExcludeFromCodeCoverage]
public class ProductRepository : GenericRepository<Model.Product>, IProductRepository
{
    public ProductRepository(Context context) : base(context)
    {
    }
}
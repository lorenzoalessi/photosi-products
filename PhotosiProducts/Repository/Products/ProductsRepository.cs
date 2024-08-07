using PhotosiProducts.Model;

namespace PhotosiProducts.Repository.Products;

public class ProductsRepository : GenericRepository<Product>, IProductsRepository
{
    public ProductsRepository(Context context) : base(context)
    {
    }
}
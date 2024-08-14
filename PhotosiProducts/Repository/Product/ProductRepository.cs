using PhotosiProducts.Model;

namespace PhotosiProducts.Repository.Product;

public class ProductRepository : GenericRepository<Model.Product>, IProductRepository
{
    public ProductRepository(Context context) : base(context)
    {
    }
}
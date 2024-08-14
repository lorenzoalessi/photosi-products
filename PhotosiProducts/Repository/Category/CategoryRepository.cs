using PhotosiProducts.Model;

namespace PhotosiProducts.Repository.Category;

public class CategoryRepository : GenericRepository<Model.Category>, ICategoryRepository
{
    public CategoryRepository(Context context) : base(context)
    {
    }
}
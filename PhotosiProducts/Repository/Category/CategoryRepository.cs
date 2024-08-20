using System.Diagnostics.CodeAnalysis;
using PhotosiProducts.Model;

namespace PhotosiProducts.Repository.Category;

// TODO Escluso dai test fino a che non implementa metodi custom
[ExcludeFromCodeCoverage]
public class CategoryRepository : GenericRepository<Model.Category>, ICategoryRepository
{
    public CategoryRepository(Context context) : base(context)
    {
    }
}
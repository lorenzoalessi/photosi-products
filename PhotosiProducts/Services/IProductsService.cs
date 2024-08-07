using PhotosiProducts.Dto;

namespace PhotosiProducts.Services;

public interface IProductsService
{
    Task<List<ProductDto>> GetAsync();
}
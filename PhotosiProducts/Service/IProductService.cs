using PhotosiProducts.Dto;

namespace PhotosiProducts.Service;

public interface IProductService
{
    Task<List<ProductDto>> GetAsync();
}
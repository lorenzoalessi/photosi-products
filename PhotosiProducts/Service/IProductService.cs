using PhotosiProducts.Dto;

namespace PhotosiProducts.Service;

public interface IProductService
{
    Task<List<ProductDto>> GetAsync();
    
    Task<ProductDto> GetByIdAsync(int id);

    Task<ProductDto> UpdateAsync(int id, ProductDto productDto);
    
    Task<ProductDto> AddAsync(ProductDto productDto);
    
    Task<bool> DeleteAsync(int id);
}
using AutoMapper;
using PhotosiProducts.Dto;
using PhotosiProducts.Exceptions;
using PhotosiProducts.Model;
using PhotosiProducts.Repository.Category;
using PhotosiProducts.Repository.Product;

namespace PhotosiProducts.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAsync()
    {
        var products = await _productRepository.GetAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateAsync(int id, ProductDto productDto)
    {
        await CheckExistingCategory(productDto.CategoryId);

        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            throw new ProductException($"Il prodotto con ID {id} non esiste");

        product.CategoryId = productDto.CategoryId;
        product.Name = productDto.Name;
        product.Description = productDto.Description;

        await _productRepository.SaveAsync();

        return productDto;
    }

    public async Task<ProductDto> AddAsync(ProductDto productDto)
    {
        await CheckExistingCategory(productDto.CategoryId);
        
        var product = _mapper.Map<Product>(productDto);
        await _productRepository.AddAsync(product);

        // Aggiorno l'Id della dto senza rimappare
        productDto.Id = product.Id;
        return productDto;
    }

    public async Task<bool> DeleteAsync(int id) => await _productRepository.DeleteAsync(id);

    private async Task CheckExistingCategory(int categoryId)
    {
        // Verifico l'esistenza della categoria associata
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null)
            throw new CategoryException($"La categoria {categoryId} non esiste");
    }
}
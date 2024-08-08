using AutoMapper;
using PhotosiProducts.Dto;
using PhotosiProducts.Repository.Products;
using PhotosiProducts.Utility;

namespace PhotosiProducts.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<List<ProductDto>> GetAsync()
    {
        var products = await _productsRepository.GetAsync();
        return products.MapTo<List<ProductDto>>();
    }
}
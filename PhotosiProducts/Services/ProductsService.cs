using AutoMapper;
using PhotosiProducts.Dto;
using PhotosiProducts.Repository.Products;

namespace PhotosiProducts.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public ProductsService(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAsync()
    {
        var products = await _productsRepository.GetAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }
}
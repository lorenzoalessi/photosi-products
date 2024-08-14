using AutoMapper;
using PhotosiProducts.Dto;
using PhotosiProducts.Repository.Product;

namespace PhotosiProducts.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAsync()
    {
        var products = await _productRepository.GetAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }
}
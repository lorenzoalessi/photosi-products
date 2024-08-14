using AutoMapper;
using Moq;
using PhotosiProducts.Mapper;
using PhotosiProducts.Model;
using PhotosiProducts.Repository.Product;
using PhotosiProducts.Service;

namespace PhotosiProducts.xUnitTest.Service;

public class ProductServiceTest : TestSetup
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly IMapper _mapper;
    
    public ProductServiceTest()
    {
        _mockProductRepository = new Mock<IProductRepository>();

        var config = new MapperConfiguration(conf =>
        {
            conf.AddProfile(typeof(ProductMapperProfile));
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task GetAsync_ShouldReturnList_Always()
    {
        // Arrange
        var service = GetService();
        
        // genero la lista casuale
        var products = Enumerable.Range(0, _faker.Int(10, 30))
            .Select(x => GenerateProduct())
            .ToList();
        _mockProductRepository.Setup(x => x.GetAsync())
            // Gli passo la mia lista
            .ReturnsAsync(products);

        // Act
        var result = await service.GetAsync();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Equal(result.Count, products.Count);
            Assert.Empty(result.Select(x => x.Id).Except(products.Select(x => x.Id)));
            Assert.Empty(result.Select(x => x.Name).Except(products.Select(x => x.Name)));
            Assert.Empty(result.Select(x => x.Description).Except(products.Select(x => x.Description)));
            Assert.Empty(result.Select(x => x.CategoryId).Except(products.Select(x => x.CategoryId)));
        });

        _mockProductRepository.Verify(x => x.GetAsync(), Times.Once);
    }

    private IProductService GetService() => new ProductService(_mockProductRepository.Object, _mapper);

    private Product GenerateProduct()
    {
        return new Product()
        {
            Id = _faker.Int(1),
            CategoryId = _faker.Int(1),
            Name = _faker.String2(1, 100),
            Description = _faker.String2(1, 100)
        };
    }
}
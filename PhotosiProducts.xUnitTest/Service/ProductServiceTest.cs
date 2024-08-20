using AutoMapper;
using Moq;
using PhotosiProducts.Dto;
using PhotosiProducts.Exceptions;
using PhotosiProducts.Mapper;
using PhotosiProducts.Model;
using PhotosiProducts.Repository.Category;
using PhotosiProducts.Repository.Product;
using PhotosiProducts.Service;

namespace PhotosiProducts.xUnitTest.Service;

public class ProductServiceTest : TestSetup
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly IMapper _mapper;
    
    public ProductServiceTest()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockCategoryRepository = new Mock<ICategoryRepository>();

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

    [Fact]
    public async Task GetByIdAsync_ShouldReturnDto_IfPresent()
    {
        // Arrange
        var service = GetService();

        var product = GenerateProduct();
        
        _mockProductRepository.Setup(x => x.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await service.GetByIdAsync(product.Id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Equal(result.Id, product.Id);
        });
        
        _mockProductRepository.Verify(x => x.GetByIdAsync(product.Id), Times.Once);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_IfNotPresent()
    {
        // Arrange
        var service = GetService();
        
        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Product) null);

        // Act
        var result = await service.GetByIdAsync(_faker.Int(1));

        // Assert
        Assert.Null(result);
        
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnDto_Always()
    {
        // Arrange
        var service = GetService();

        var product = GenerateProduct();

        var productDto = new ProductDto()
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            Name = product.Name,
            Description = product.Description
        };

        _mockCategoryRepository.Setup(x => x.GetByIdAsync(productDto.CategoryId))
            .ReturnsAsync(new Category());
        _mockProductRepository.Setup(x => x.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await service.UpdateAsync(productDto.Id, productDto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        });
        
        _mockCategoryRepository.Verify(x => x.GetByIdAsync(productDto.CategoryId), Times.Once);
        _mockProductRepository.Verify(x => x.GetByIdAsync(productDto.Id), Times.Once);
    }

    [Fact]
    public void UpdateAsync_ShouldThrowException_IfCategoryNotExist()
    {
        // Arrange
        var service = GetService();
        
        var productDto = new ProductDto()
        {
            CategoryId = _faker.Int(1)
        };
        
        // Act
        Assert.ThrowsAsync<CategoryException>(async () => await service.UpdateAsync(productDto.Id, productDto));
        
        // Assert
        _mockCategoryRepository.Verify(x => x.GetByIdAsync(productDto.CategoryId), Times.Once);
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
    }
    
    [Fact]
    public void UpdateAsync_ShouldThrowException_IfProductNotExist()
    {
        // Arrange
        var service = GetService();
        
        var productDto = new ProductDto()
        {
            Id = _faker.Int(1),
            CategoryId = _faker.Int(1)
        };
        
        _mockCategoryRepository.Setup(x => x.GetByIdAsync(productDto.CategoryId))
            .ReturnsAsync(new Category());
        
        // Act
        Assert.ThrowsAsync<ProductException>(async () => await service.UpdateAsync(productDto.Id, productDto));
        
        // Assert
        _mockCategoryRepository.Verify(x => x.GetByIdAsync(productDto.CategoryId), Times.Once);
        _mockProductRepository.Verify(x => x.GetByIdAsync(productDto.Id), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnDto_Always()
    {
        // Arrange
        var service = GetService();

        var product = GenerateProduct();

        var productDto = new ProductDto()
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            Name = product.Name,
            Description = product.Description
        };

        _mockCategoryRepository.Setup(x => x.GetByIdAsync(productDto.CategoryId))
            .ReturnsAsync(new Category());
        _mockProductRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(product);

        // Act
        var result = await service.AddAsync(productDto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        });
        
        _mockCategoryRepository.Verify(x => x.GetByIdAsync(productDto.CategoryId), Times.Once);
        _mockProductRepository.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
    }
    
    [Fact]
    public void AddAsync_ShouldThrowException_IfCategoryNotExist()
    {
        // Arrange
        var service = GetService();
        
        var productDto = new ProductDto()
        {
            CategoryId = _faker.Int(1)
        };
        
        // Act
        Assert.ThrowsAsync<CategoryException>(async () => await service.AddAsync(productDto));
        
        // Assert
        _mockCategoryRepository.Verify(x => x.GetByIdAsync(productDto.CategoryId), Times.Once);
        _mockProductRepository.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Never);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DeleteAsync_ShouldReturnBool_Always(bool deteleSuccess)
    {
        // Arrange
        var service = GetService();

        var productId = _faker.Int(1);
        var product = GenerateProduct();
        
        if (deteleSuccess)
        {
            productId = product.Id;
        }

        _mockProductRepository.Setup(x => x.DeleteAsync(productId))
            .ReturnsAsync(deteleSuccess);

        // Act
        var result = await service.DeleteAsync(productId);

        // Assert
        Assert.Equal(result, deteleSuccess);
        
        _mockProductRepository.Verify(x => x.DeleteAsync(productId), Times.Once);
    }

    private IProductService GetService() => new ProductService(_mockProductRepository.Object, _mockCategoryRepository.Object, _mapper);

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
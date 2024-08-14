using PhotosiProducts.Model;
using PhotosiProducts.Repository;

namespace PhotosiProducts.xUnitTest.Repository;

public class GenericRepositoryTest : TestSetup
{
    [Fact]
    public async Task GetAsync_ShouldReturnList_Always()
    {
        // Arrange
        var repository = GetRepository();

        var products = Enumerable.Range(0, _faker.Int(10, 30))
            .Select(x => GenerateProductAndSave())
            .ToList();

        // Act
        var result = await repository.GetAsync();

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
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity_IfPresent()
    {
        // Arrange
        var repository = GetRepository();

        var product = GenerateProductAndSave();

        // Act
        var result = await repository.GetByIdAsync(product.Id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Equal(result.Id, product.Id);
        });
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_IfNotPresent()
    {
        // Arrange
        var repository = GetRepository();
        
        // Act
        var result = await repository.GetByIdAsync(_faker.Int(2));

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnEntity_Always()
    {
        // Arrange
        var repository = GetRepository();
        
        // Act
        var result = await repository.AddAsync(GenerateProduct());

        // Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        });
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DeleteAsync_ShouldReturnBool_Always(bool deleteSuccess)
    {
        // Arrange
        var repository = GetRepository();

        var productId = _faker.Int(1);

        if (deleteSuccess)
        {
            var product = GenerateProductAndSave();
            productId = product.Id;
        }
        
        // Act
        var result = await repository.DeleteAsync(productId);
            
        // Assert
        Assert.Equal(result, deleteSuccess);
    }

    private IGenericRepository<Product> GetRepository() => new GenericRepository<Product>(_context);

    private Product GenerateProductAndSave()
    {
        var product = GenerateProduct();

        _context.Add(product);
        _context.SaveChanges();

        return product;
    }

    private Product GenerateProduct()
    {
        return new Product()
        {
            CategoryId = _faker.Int(1),
            Name = _faker.String2(1, 100),
            Description = _faker.String2(1, 100)
        };
    }
}
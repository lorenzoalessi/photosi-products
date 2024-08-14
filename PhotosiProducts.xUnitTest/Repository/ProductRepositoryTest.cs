using PhotosiProducts.Model;
using PhotosiProducts.Repository.Product;

namespace PhotosiProducts.xUnitTest.Repository;

public class ProductRepositoryTest : TestSetup
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

    private IProductRepository GetRepository() => new ProductRepository(_context);

    private Product GenerateProductAndSave()
    {
        var product = new Product()
        {
            Id = _faker.Int(1),
            CategoryId = _faker.Int(1),
            Name = _faker.String2(1, 100),
            Description = _faker.String2(1, 100)
        };

        _context.Add(product);
        _context.SaveChanges();

        return product;
    }
}
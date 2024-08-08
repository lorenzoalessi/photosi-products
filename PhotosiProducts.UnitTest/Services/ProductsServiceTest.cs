using Moq;
using PhotosiProducts.Model;
using PhotosiProducts.Repository.Products;
using PhotosiProducts.Services;

namespace PhotosiProducts.UnitTest.Services;

[TestFixture]
public class ProductsServiceTest : TestSetup
{
    private Mock<IProductsRepository> _mockProductRepository;
    
    [SetUp]
    protected override void SetUp()
    {
        base.SetUp();

        _mockProductRepository = new Mock<IProductsRepository>();
    }

    [Test]
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(products.Count));
            Assert.That(result.Select(x => x.Id).Except(products.Select(x => x.Id)), Is.Empty);
            Assert.That(result.Select(x => x.Name).Except(products.Select(x => x.Name)), Is.Empty);
            Assert.That(result.Select(x => x.Description).Except(products.Select(x => x.Description)), Is.Empty);
            Assert.That(result.Select(x => x.CategoryId).Except(products.Select(x => x.CategoryId)), Is.Empty);
        });

        _mockProductRepository.Verify(x => x.GetAsync(), Times.Once);
    }

    private IProductsService GetService() => new ProductsService(_mockProductRepository.Object);

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
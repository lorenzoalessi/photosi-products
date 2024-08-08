using PhotosiProducts.Model;
using PhotosiProducts.Repository.Products;

namespace PhotosiProducts.UnitTest.Repository;

[TestFixture] // Serve se ha uno o più test
public class ProductsRepositoryTest : TestSetup
{
    [SetUp] // Tutti i metodi con decoratore [Test] chiamano in automatico i metodi con questo decoratore
    protected override void SetUp()
    {
        base.SetUp();
    }

    [Test]
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(products.Count));
            Assert.That(result.Select(x => x.Id).Except(products.Select(x => x.Id)), Is.Empty);
            Assert.That(result.Select(x => x.Name).Except(products.Select(x => x.Name)), Is.Empty);
            Assert.That(result.Select(x => x.Description).Except(products.Select(x => x.Description)), Is.Empty);
            Assert.That(result.Select(x => x.CategoryId).Except(products.Select(x => x.CategoryId)), Is.Empty);
        });
    }
    
    private IProductsRepository GetRepository() => new ProductsRepository(_context);
    
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
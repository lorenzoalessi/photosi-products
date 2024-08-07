namespace PhotosiProducts.Repository;

public interface IGenericRepository<TDbEntity>
{
    Task<List<TDbEntity>> GetAsync();
}
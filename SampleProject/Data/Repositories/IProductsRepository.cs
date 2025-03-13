using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductsRepository : IInMemoryRepository<Product>
    {
        bool IsExists(string name);
    }
}

using BusinessEntities;
using Common;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductsRepository : InMemoryRepository<Product>, IProductsRepository
    {
        public ProductsRepository(IMemoryCache memoryCache) : base(memoryCache, "Products")
        {
        }

        public bool IsExists(string name)
        {
            var all = Get(x => !x.DeleteDate.HasValue);

            return all.Any(x => string.Equals(x.Name, name, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}

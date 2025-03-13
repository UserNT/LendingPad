using BusinessEntities;
using Common;
using Microsoft.Extensions.Caching.Memory;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrdersRepository : InMemoryRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(IMemoryCache memoryCache) : base(memoryCache, "Orders")
        {
        }
    }
}

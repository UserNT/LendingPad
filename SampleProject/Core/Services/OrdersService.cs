using Common;
using Data.Repositories;

namespace Core.Services
{
    [AutoRegister]
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IProductsService productsService;

        public OrdersService(IOrdersRepository ordersRepository, IProductsService productsService)
        {
            this.ordersRepository = ordersRepository;
            this.productsService = productsService;
        }
    }
}

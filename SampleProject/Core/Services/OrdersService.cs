using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;

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

        public void DeleteAll()
        {
            var orders = ordersRepository.Get(x => !x.DeleteDate.HasValue);

            foreach (var order in orders)
            {
                Delete(order);
            }
        }

        public void Delete(Order order)
        {
            order.MarkAsDeleted();
            ordersRepository.Save(order);
        }

        public Order Get(Guid id)
        {
            return ordersRepository.Get(id);
        }

        public IEnumerable<Order> GetAll(bool includeDeleted)
        {
            if (includeDeleted)
                return ordersRepository.GetAll();

            return ordersRepository.Get(x => !x.DeleteDate.HasValue);
        }

        public Order Create(Dictionary<Guid, int> items)
        {
            var entity = new Order();

            foreach (var item in items)
            {
                var product = productsService.Get(item.Key);

                if (product == null)
                {
                    return null;
                }

                entity.AddItem(product, item.Value);
            }

            ordersRepository.Save(entity);

            return entity;
        }

        public IEnumerable<Order> Get(Predicate<Order> predicate, Func<Order, object> orderBy, bool isDescOrder, int skip, int take)
        {
            return ordersRepository.Get(predicate, orderBy, isDescOrder, skip, take);
        }
    }
}

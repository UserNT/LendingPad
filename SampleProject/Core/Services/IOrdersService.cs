using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services
{
    public interface IOrdersService
    {
        void DeleteAll();

        void Delete(Order order);

        Order Get(Guid id);

        IEnumerable<Order> GetAll(bool includeDeleted);
        
        Order Create(Dictionary<Guid, int> items);

        IEnumerable<Order> Get(Predicate<Order> predicate, Func<Order, object> orderBy, bool isDescOrder, int skip, int take);
    }
}

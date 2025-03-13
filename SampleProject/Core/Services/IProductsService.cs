using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services
{
    public interface IProductsService
    {
        Product Create(string name, decimal price, string description);

        void DeleteAll();

        void Delete(Product product);

        IEnumerable<Product> GetAll(bool includeDeleted);

        IEnumerable<Product> Get(Predicate<Product> predicate, Func<Product, object> orderBy, bool isDescOrder, int skip, int take);

        Product Get(Guid id);
        
        void Update(Product entity, decimal price, string description);
    }
}

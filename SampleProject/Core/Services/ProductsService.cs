using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Core.Services
{
    [AutoRegister]
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public Product Create(string name, decimal price, string description)
        {
            if (productsRepository.IsExists(name))
            {
                return null;
            }

            var product = new Product() 
            {
                Price = price,
                Description = description
            };
            product.SetName(name);

            productsRepository.Save(product);

            return product;
        }

        public void DeleteAll()
        {
            var products = productsRepository.Get(x => !x.DeleteDate.HasValue);

            foreach (var product in products)
            {
                Delete(product);
            }
        }

        public void Delete(Product product)
        {
            product.MarkAsDeleted();
            productsRepository.Save(product);
        }

        public Product Get(Guid id)
        {
            return productsRepository.Get(id);
        }

        public IEnumerable<Product> GetAll(bool includeDeleted)
        {
            if (includeDeleted)
                return productsRepository.GetAll();

            return productsRepository.Get(x => !x.DeleteDate.HasValue);
        }

        public void Update(Product entity, decimal price, string description)
        {
            entity.Price = price;
            entity.Description = description;

            productsRepository.Save(entity);
        }
    }
}

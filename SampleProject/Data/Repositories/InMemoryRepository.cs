using BusinessEntities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public abstract class InMemoryRepository<T> : IInMemoryRepository<T> where T : IdObject
    {
        private readonly IMemoryCache memoryCache;
        private readonly string tableName;

        protected InMemoryRepository(IMemoryCache memoryCache, string tableName)
        {
            this.memoryCache = memoryCache;
            this.tableName = tableName;
        }

        public IEnumerable<T> GetAll()
        {
            if (memoryCache.TryGetValue(tableName, out var obj) &&
                obj is ConcurrentDictionary<Guid, T> table)
            {
                return table.Values;
            }

            return Enumerable.Empty<T>();
        }

        public IEnumerable<T> Get(Predicate<T> predicate)
        {
            return GetAll().Where(x => predicate(x));
        }

        public IEnumerable<T> Get(Predicate<T> predicate, Func<T, object> orderBy, bool isDescOrder, int skip, int take)
        {
            var query = GetAll().Where(x => predicate(x));
            
            if (isDescOrder)
                query = query.OrderByDescending(orderBy);
            else
                query = query.OrderBy(orderBy);
            
            return query.Skip(skip).Take(take);
        }

        public T Get(Guid id)
        {
            if (memoryCache.TryGetValue(tableName, out var obj) &&
                obj is ConcurrentDictionary<Guid, T> table &&
                table.TryGetValue(id, out var entity))
            {
                return entity;
            }

            return default;
        }

        public void Save(T entity)
        {
            var table = memoryCache.GetOrCreate(tableName, (key) => new ConcurrentDictionary<Guid, T>());

            table.AddOrUpdate(entity.Id, entity, (key, value) => entity);
        }

        public void Delete(T entity)
        {
            if (memoryCache.TryGetValue(tableName, out var obj) && 
                obj is ConcurrentDictionary<Guid, T> table)
            {
                table.TryRemove(entity.Id, out var _);
            }
        }
    }
}

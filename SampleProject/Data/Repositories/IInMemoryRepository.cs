using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IInMemoryRepository<T> where T : IdObject
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Get(Predicate<T> predicate);

        IEnumerable<T> Get(Predicate<T> predicate, Func<T, object> orderBy, bool isDescOrder, int skip, int take);
        
        T Get(Guid id);

        void Delete(T entity);

        void Save(T entity);
    }
}

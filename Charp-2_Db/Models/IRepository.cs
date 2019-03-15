using System;
using System.Collections.Generic;

namespace Charp_2_Db.Models
{
    public interface IRepository<T>
    {
        void Create(T item);
        T Retrieve(int id);
        IEnumerable<T> RetrieveMultiple(Func<T,bool> predicate);
        void Update(T item);
        void Delete(T item);
        void Save();
        void Save(IEnumerable<T> items);
    }
}
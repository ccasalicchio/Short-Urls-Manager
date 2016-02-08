using System.Collections.Generic;

namespace Shorten_Urls.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        void Add(T element);
        void Update(T element);
        void Remove(int id);
        void RemoveAll();
        void Remove(T element);
    }
}

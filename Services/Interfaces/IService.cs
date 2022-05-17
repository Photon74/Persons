using System.Collections.Generic;

namespace Persons.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        T GetById(int id);
        IReadOnlyList<T> GetByName(string name);
        IReadOnlyList<T> GetItemsList(int skip, int take);
        bool AddItem(T item);
        bool UpdateItem(T item);
        bool DeleteItem(int id);

    }
}

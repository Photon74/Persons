using System.Collections.Generic;

namespace Persons.DAL.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T GetItem(int id);
        IReadOnlyList<T> GetItemsList(int skip, int take);
        IReadOnlyList<T> FindItems(string searchQuery);
        bool AddItem(T item);
        bool UpdateItem(int id, T item);
        bool DeleteItem(int id);
    }
}

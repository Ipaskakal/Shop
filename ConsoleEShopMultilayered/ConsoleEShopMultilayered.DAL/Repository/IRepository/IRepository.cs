using System.Collections.Generic;

namespace ConsoleEShopMultilayered.DAL.Repository.IRepository
{
    public interface IRepository<T>
    {
        List<T> GetAll();

        bool Add(T item);

        T FindItemByKey(string key);

        T FindItemById(int Id);
        T Update(T oldItem, T newItem);
    }
}

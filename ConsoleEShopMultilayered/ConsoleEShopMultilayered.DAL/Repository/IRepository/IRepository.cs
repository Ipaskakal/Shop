using System.Collections.Generic;

namespace ConsoleEShopMultilayered.DAL.Repository.IRepository
{
    /// <summary>
    /// Interface for repository
    /// </summary>  
    public interface IRepository<T>
    {
        /// <summary>
        /// Get list of all elements
        /// </summary>  
        List<T> GetAll();

        /// <summary>
        /// Save element
        /// </summary>  
        /// <param name="item">element to save</param>
        bool Add(T item);

        /// <summary>
        /// Get element by key
        /// </summary>  
        /// <param name="key">key for search</param>
        T FindItemByKey(string key);

        /// <summary>
        /// Get element by id
        /// </summary>  
        /// <param name="Id">id for search</param>
        T FindItemById(int Id);

        /// <summary>
        /// update and return resulted element
        /// </summary>  
        /// <param name="oldItem">element to delete</param>
        ///  <param name="newItem">element to save</param>
        T Update(T oldItem, T newItem);
    }
}

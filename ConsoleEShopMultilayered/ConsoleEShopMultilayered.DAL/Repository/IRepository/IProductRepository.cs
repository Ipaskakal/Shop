using ConsoleEShopMultilayered.DAL.Models;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.DAL.Repository.IRepository
{
    /// <summary>
    /// Interface for product repository
    /// </summary>  
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Get list of products by user name
        /// </summary>  
        /// <param name="name">user name</param>
        List<Product> GetProductsByName(string name);
    }
}

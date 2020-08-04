using ConsoleEShopMultilayered.DAL.Models;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.DAL.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProductsByName(string name);
    }
}

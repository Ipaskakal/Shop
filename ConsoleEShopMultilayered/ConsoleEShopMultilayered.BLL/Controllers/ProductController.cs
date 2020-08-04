using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.BLL.Controllers
{
    public class ProductController
    {
        readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<Product> GetListOfProducts()
        {
            return productRepository.GetAll();
        }

        public bool AddProduct(Product product)
        {
            return productRepository.Add(product);
        }
        public List<Product> FindProductsbyName(string name)
        {
            return productRepository.GetProductsByName(name);
        }

        public Product GetProductByKey(string id)
        {
            return this.productRepository.FindItemByKey(id);
        }

    }
}
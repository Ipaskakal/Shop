using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.BLL.Controllers
{
    /// <summary>
    /// Controller for requests about products
    /// </summary> 
    public class ProductController
    {
        readonly IProductRepository productRepository;


        /// <summary>
        /// Constructor that gets product repositoriy
        /// </summary> 
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Fet all products
        /// </summary> 
        public List<Product> GetListOfProducts()
        {
            return productRepository.GetAll();
        }


        /// <summary>
        /// Try to add new product
        /// </summary> 
        public bool AddProduct(Product product)
        {
            return productRepository.Add(product);
        }

        /// <summary>
        /// Get existing products by part of name
        /// </summary> 
        public List<Product> FindProductsbyName(string name)
        {
            return productRepository.GetProductsByName(name);
        }

        /// <summary>
        /// Get product by id
        /// </summary> 
        public Product GetProductByKey(string id)
        {
            return this.productRepository.FindItemByKey(id);
        }

    }
}
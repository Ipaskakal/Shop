using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using Moq;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.DAL.Repository
{
    class MockProductRepository
    {
        static readonly List<Product> products = new List<Product>
        {
            new Product() { Id =1, Name="tomatoes", Description="nice", Price=123},
            new Product() { Id=2, Name="Max", Description="123", Price=321},

        };

        public IProductRepository ProductRepository { get; set; }
        public MockProductRepository()
        {

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(mq => mq.Add(It.IsAny<Product>()))
                .Returns<Product>(product =>
                {
                    product.Id = products[products.Count - 1].Id + 1;
                    products.Add(product);
                    return true;
                });
            productRepositoryMock.Setup(mq => mq.FindItemById(It.IsAny<int>()))
                .Returns<int>(productId =>
                {
                    foreach (var tempProduct in products)
                        if (productId == tempProduct.Id) return tempProduct;
                    return null;
                }
                );
            productRepositoryMock.Setup(mq => mq.GetAll()).Returns(products);
            productRepositoryMock.Setup(mq => mq.GetProductsByName(It.IsAny<string>()))
                .Returns<string>(product =>
                {
                    List<Product> result = new List<Product>();
                    foreach (var tempProduct in products)
                        if (tempProduct.Name.IndexOf(product) != -1) result.Add(tempProduct);
                    return result;
                }
                );
            this.ProductRepository = productRepositoryMock.Object;

        }

    }
}

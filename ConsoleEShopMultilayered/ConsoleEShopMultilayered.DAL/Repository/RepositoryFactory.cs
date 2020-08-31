using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using Moq;

namespace ConsoleEShopMultilayered.DAL.Repository
{
    /// <summary>
    /// Factory of repositories
    /// </summary>  
    public class RepositoryFactory
    {

        RepositoryType UserRepositoryType { get; set; }
        RepositoryType ProductRepositoryType { get; set; }
        RepositoryType OrderRepositoryType { get; set; }

        /// <summary>
        /// Sets types of repositories that will be created
        /// </summary>  
        public RepositoryFactory(RepositoryType userRepositoryType, RepositoryType productRepositoryType, RepositoryType orderRepositoryType)
        {
            this.UserRepositoryType = userRepositoryType;
            this.ProductRepositoryType = productRepositoryType;
            this.OrderRepositoryType = orderRepositoryType;
        }

        /// <summary>
        /// Create user repository
        /// </summary>  
        public IUserRepository GetUserRepository()
        {
            if (this.UserRepositoryType == RepositoryType.Mock)
                return new MockUserRepository().UserRepository;
            else if (this.UserRepositoryType == RepositoryType.SQL)
                return new Mock<IUserRepository>().Object;
            else
                return new Mock<IUserRepository>().Object;
        }

        /// <summary>
        /// Create product repository
        /// </summary>  
        public IProductRepository GetProductRepository()
        {
            if (this.ProductRepositoryType == RepositoryType.Mock)
                return new MockProductRepository().ProductRepository;
            else if (this.ProductRepositoryType == RepositoryType.SQL)
                return new Mock<IProductRepository>().Object;
            else
                return new Mock<IProductRepository>().Object;
        }

        /// <summary>
        /// Create order repository
        /// </summary>  
        public IOrderRepository GetOrderRepository()
        {
            if (this.OrderRepositoryType == RepositoryType.Mock)
                return new MockOrderRepository().OrderRepository;
            else if (this.OrderRepositoryType == RepositoryType.SQL)
                return new Mock<IOrderRepository>().Object;
            else
                return new Mock<IOrderRepository>().Object;
        }
    }
}

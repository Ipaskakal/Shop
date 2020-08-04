using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using Moq;

namespace ConsoleEShopMultilayered.DAL.Repository
{
    public class RepositoryFactory
    {

        RepositoryType UserRepositoryType { get; set; }
        RepositoryType ProductRepositoryType { get; set; }
        RepositoryType OrderRepositoryType { get; set; }

        public RepositoryFactory(RepositoryType userRepositoryType, RepositoryType productRepositoryType, RepositoryType orderRepositoryType)
        {
            this.UserRepositoryType = userRepositoryType;
            this.ProductRepositoryType = productRepositoryType;
            this.OrderRepositoryType = orderRepositoryType;
        }

        public IUserRepository GetUserRepository()
        {
            if (this.UserRepositoryType == RepositoryType.Mock)
                return new MockUserRepository().UserRepository;
            else if (this.UserRepositoryType == RepositoryType.SQL)
                return new Mock<IUserRepository>().Object;
            else
                return new Mock<IUserRepository>().Object;
        }

        public IProductRepository GetProductRepository()
        {
            if (this.ProductRepositoryType == RepositoryType.Mock)
                return new MockProductRepository().ProductRepository;
            else if (this.ProductRepositoryType == RepositoryType.SQL)
                return new Mock<IProductRepository>().Object;
            else
                return new Mock<IProductRepository>().Object;
        }

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

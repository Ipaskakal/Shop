using ConsoleEShopMultilayered.DAL.Repository;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;

namespace ConsoleEShopMultilayered.DAL
{
    public class UnitOfWork
    {
        public readonly IProductRepository productRepository;
        public readonly IUserRepository userRepository;
        public readonly IOrderRepository orderRepository;


        public UnitOfWork()
        {
            RepositoryFactory repositoryFactory = new RepositoryFactory(RepositoryType.Mock,
                RepositoryType.Mock, RepositoryType.Mock);
            productRepository = repositoryFactory.GetProductRepository();
            orderRepository = repositoryFactory.GetOrderRepository();
            userRepository = repositoryFactory.GetUserRepository();
        }
    }
}

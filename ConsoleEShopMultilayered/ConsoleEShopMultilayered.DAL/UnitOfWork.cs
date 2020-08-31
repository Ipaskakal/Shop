using ConsoleEShopMultilayered.DAL.Repository;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;

namespace ConsoleEShopMultilayered.DAL
{
    /// <summary>
    /// class unit of work for repositories
    ///  </summary>  
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Repository that handle data about products 
        /// </summary>  
        public IProductRepository ProductRepository { get; }

        /// <summary>
        /// Repository that handle data about users 
        /// </summary>  
        public IUserRepository UserRepository { get; }

        /// <summary>
        /// Repository that handle data about orders 
        /// </summary>  
        public IOrderRepository OrderRepository { get; }


        /// <summary>
        /// Constructor that init repositories
        /// </summary>  
        public UnitOfWork()
        {
            RepositoryFactory repositoryFactory = new RepositoryFactory(RepositoryType.Mock,
                RepositoryType.Mock, RepositoryType.Mock);
            ProductRepository = repositoryFactory.GetProductRepository();
            OrderRepository = repositoryFactory.GetOrderRepository();
            UserRepository = repositoryFactory.GetUserRepository();
        }


    }
}

using ConsoleEShopMultilayered.DAL.Repository.IRepository;

namespace ConsoleEShopMultilayered.DAL
{
    /// <summary>
    /// Interface unit of work for repositories
    /// </summary>  
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repository that handle data about products 
        /// </summary>  
        IProductRepository ProductRepository { get; }

        /// <summary>
        /// Repository that handle data about users 
        /// </summary>  
        IUserRepository UserRepository { get; }

        /// <summary>
        /// Repository that handle data about orders 
        /// </summary>  
        IOrderRepository OrderRepository { get; }
    }
}

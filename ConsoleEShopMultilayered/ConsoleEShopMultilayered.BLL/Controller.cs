using ConsoleEShopMultilayered.BLL.Controllers;
using ConsoleEShopMultilayered.DAL;
using Ninject;

namespace ConsoleEShopMultilayered.BLL
{
    /// <summary>
    /// Controller that incapsulate all BLL layer
    /// </summary> 
    public class Controller:IController
    {
        /// <summary>
        /// Controller for requests about users
        /// </summary> 
        public UserController UserController { get; }

        /// <summary>
        /// Controller for requests about products
        /// </summary> 
        public ProductController ProductController { get; }

        /// <summary>
        /// Controller for requests about orders
        /// </summary> 
        public OrderController OrderController { get; }

        /// <summary>
        /// Constructor that init controllers and bind with DAL
        /// </summary> 
        public Controller()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            IUnitOfWork unitOfWork = kernel.Get<IUnitOfWork>();
            this.UserController = new UserController(unitOfWork.UserRepository);
            this.ProductController = new ProductController(unitOfWork.ProductRepository);
            this.OrderController = new OrderController(unitOfWork.ProductRepository,
                unitOfWork.UserRepository, unitOfWork.OrderRepository);
        }

    }
}

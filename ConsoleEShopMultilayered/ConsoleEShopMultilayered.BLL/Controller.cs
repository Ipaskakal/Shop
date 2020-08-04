using ConsoleEShopMultilayered.BLL.Controllers;
using ConsoleEShopMultilayered.DAL;

namespace ConsoleEShopMultilayered.BLL
{
    public class Controller
    {
        public UserController UserController { get; }
        public ProductController ProductController { get; }
        public OrderController OrderController { get; }

        public Controller()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            this.UserController = new UserController(unitOfWork.userRepository);
            this.ProductController = new ProductController(unitOfWork.productRepository);
            this.OrderController = new OrderController(unitOfWork.productRepository, unitOfWork.userRepository, unitOfWork.orderRepository);
        }

    }
}

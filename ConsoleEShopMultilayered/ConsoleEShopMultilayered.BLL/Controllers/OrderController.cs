using ConsoleEShopMultilayered.BLL.VievModels;
using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.BLL.Controllers
{
    /// <summary>
    /// Controller for requests about orders
    /// </summary> 
    public class OrderController
    {
        readonly IProductRepository productRepository;
        readonly IUserRepository userRepository;
        readonly IOrderRepository orderRepository;

        /// <summary>
        /// Constructor that gets user, product, order repositories
        /// </summary> 
        public OrderController(IProductRepository productRepository, IUserRepository userRepository, IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
        }

        /// <summary>
        /// add new order
        /// </summary> 
        public void AddOrder(OrderViewModel viewOrder)
        {
            Order order = Mapping.Mapper.Map<OrderViewModel, Order>(viewOrder);
            order.IdUser = userRepository.FindItemByKey(viewOrder.EmailUser).Id;
            this.orderRepository.Add(order);
        }

        /// <summary>
        /// Get orders of user
        /// </summary> 
        public List<OrderViewModel> GetOrdersOfUser(string email)
        {
            var user = this.userRepository.FindItemByKey(email);
            var orders = this.orderRepository.GetAllUserOrdersByIdUser(user.Id);
            List<OrderViewModel> viewOrders = Mapping.Mapper.Map<List<Order>, List<OrderViewModel>>(orders);
            foreach (var viewOrder in viewOrders)
            {
                viewOrder.Price = this.productRepository.FindItemById(viewOrder.IdProduct).Price;
                viewOrder.EmailUser = user.Email;
            }
            return viewOrders;
        }

        /// <summary>
        ///Get order by order id
        /// </summary> 
        public OrderViewModel GetOrderById(int id)
        {
            var order = this.orderRepository.FindItemById(id);
            OrderViewModel viewOrder = Mapping.Mapper.Map<Order, OrderViewModel>(order);
            viewOrder.EmailUser = this.userRepository.FindItemById(id).Email;
            viewOrder.Price = this.productRepository.FindItemById(viewOrder.IdProduct).Price;
            return viewOrder;
        }

        /// <summary>
        /// Update state of order
        /// </summary> 
        public bool UpdateStatebyId(int id, OrderState state)
        {
            return this.orderRepository.UpdateState(id, state);
        }
    }
}
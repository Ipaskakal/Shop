using ConsoleEShopMultilayered.DAL.Models;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.DAL.Repository.IRepository
{
    /// <summary>
    /// Interface for order repository
    /// </summary>  
    public interface IOrderRepository : IRepository<Order>
    {
        /// <summary>
        /// Returns list of orders for user
        /// </summary>  
        /// <param name="IdUser">id of user</param>
        List<Order> GetAllUserOrdersByIdUser(int IdUser);

        /// <summary>
        /// update order state
        /// </summary>  
        ///  <param name="id">id of orde</param>
        ///  <param name="state">new state</param>
        bool UpdateState(int id, OrderState state);
    }
}

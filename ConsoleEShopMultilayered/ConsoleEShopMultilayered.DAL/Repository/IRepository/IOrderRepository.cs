using ConsoleEShopMultilayered.DAL.Models;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.DAL.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetAllUserOrdersByIdUser(int IdUser);
        bool UpdateState(int id, OrderState state);
    }
}

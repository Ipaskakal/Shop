using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using Moq;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.DAL.Repository
{
    class MockOrderRepository
    {
        static readonly List<Order> orders = new List<Order>
        {
            new Order() { Id =1, IdProduct=1, IdUser=2, State=OrderState.Sent,Name="tomatoes",Description="nice"},
            new Order() { Id=2, IdProduct=2, IdUser=3, State=OrderState.Paid,Name="Max",Description="fff"},

        };

        public IOrderRepository OrderRepository { get; set; }
        public MockOrderRepository()
        {

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(mq => mq.Add(It.IsAny<Order>()))
                .Returns<Order>(order =>
                {
                    order.Id = orders[orders.Count - 1].Id + 1;
                    orders.Add(order);
                    return true;
                });
            orderRepositoryMock.Setup(mq => mq.UpdateState(It.IsAny<int>(), It.IsAny<OrderState>()))
                .Returns<int, OrderState>((orderId, orderState) =>
                {
                    foreach (var order in orders)
                        if (orderId == order.Id)
                        {
                            order.State = orderState;
                            return true;
                        }
                    return false;
                }
               );
            orderRepositoryMock.Setup(mq => mq.GetAll()).Returns(orders);
            orderRepositoryMock.Setup(mq => mq.FindItemById(It.IsAny<int>()))
               .Returns<int>(orderId =>
               {
                   foreach (var order in orders)
                       if (orderId == order.Id) return order;
                   return null;
               }
               );
            orderRepositoryMock.Setup(mq => mq.GetAllUserOrdersByIdUser(It.IsAny<int>()))
                .Returns<int>(idUser =>
                {
                    List<Order> result = new List<Order>();
                    foreach (var order in orders)
                        if (order.IdUser == idUser) result.Add(order);
                    return result;
                });
            this.OrderRepository = orderRepositoryMock.Object;
        }
    }
}

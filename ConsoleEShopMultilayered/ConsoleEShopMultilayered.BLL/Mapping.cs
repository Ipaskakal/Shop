using AutoMapper;
using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.BLL.VievModels;

namespace ConsoleEShopMultilayered.BLL
{
    public static class Mapping
    {
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<RegisterUserViewModel, IRegisteredUser>();
            cfg.CreateMap<RegisterUserViewModel, RegisteredUser>();
            cfg.CreateMap<IRegisteredUser, UsersViewModel>();
            cfg.CreateMap<UsersViewModel, IRegisteredUser>();
            cfg.CreateMap<Order, OrderViewModel>();
            cfg.CreateMap<OrderViewModel, Order>();
        });

        public static Mapper Mapper { get; set; } = new Mapper(config);
    }


}

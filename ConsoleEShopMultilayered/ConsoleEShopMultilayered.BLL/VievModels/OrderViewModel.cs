using ConsoleEShopMultilayered.DAL.Models;

namespace ConsoleEShopMultilayered.BLL.VievModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string EmailUser { get; set; }

        public int IdProduct { get; set; }

        public int Price { get; set; }

        public OrderState State { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";
    }
}
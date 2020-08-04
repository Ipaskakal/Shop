namespace ConsoleEShopMultilayered.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdProduct { get; set; }

        public OrderState State { get; set; } = OrderState.New;

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";
    }
}

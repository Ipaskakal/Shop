namespace ConsoleEShopMultilayered.DAL.Models
{
    /// <summary>
    /// Model for ordes
    /// </summary>  
    public class Order
    {
        /// <summary>
        /// id
        /// </summary>  
        public int Id { get; set; }

        /// <summary>
        /// id of user that create this order
        /// </summary>  
        public int IdUser { get; set; }

        /// <summary>
        /// id of product that ordered
        /// </summary>  
        public int IdProduct { get; set; }

        /// <summary>
        /// order state
        /// </summary>  
        public OrderState State { get; set; } = OrderState.New;

        /// <summary>
        /// short name of order
        /// </summary>  
        public string Name { get; set; } = "";

        /// <summary>
        /// description 
        /// </summary>  
        public string Description { get; set; } = "";
    }
}

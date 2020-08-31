namespace ConsoleEShopMultilayered.DAL.Models
{
    /// <summary>
    /// Model for product
    /// </summary>  
    public class Product
    {
        /// <summary>
        /// id
        /// </summary>  
        public int Id { get;  set; }

        /// <summary>
        /// price
        /// </summary>  
        public int Price { get;  set; }

        /// <summary>
        /// short name
        /// </summary>  
        public string Name { get;  set; } = "";

        /// <summary>
        /// description
        /// </summary>  
        public string Description { get; set; } = "";
    }
}

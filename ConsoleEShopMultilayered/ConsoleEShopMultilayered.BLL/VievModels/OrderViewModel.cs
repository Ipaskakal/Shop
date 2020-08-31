using ConsoleEShopMultilayered.DAL.Models;

namespace ConsoleEShopMultilayered.BLL.VievModels
{
    /// <summary>
    /// model to output order
    /// </summary> 
    public class OrderViewModel
    {
        /// <summary>
        /// id
        /// </summary> 
        public int Id { get; set; }


        /// <summary>
        /// email user
        /// </summary> 
        public string EmailUser { get; set; }


        /// <summary>
        /// id product
        /// </summary> 
        public int IdProduct { get; set; }

        /// <summary>
        /// price
        /// </summary> 
        public int Price { get; set; }

        /// <summary>
        /// state
        /// </summary> 
        public OrderState State { get; set; }

        /// <summary>
        /// name
        /// </summary> 
        public string Name { get; set; } = "";

        /// <summary>
        /// description
        /// </summary> 
        public string Description { get; set; } = "";
    }
}
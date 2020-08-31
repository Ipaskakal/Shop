using ConsoleEShopMultilayered.BLL.Controllers;

namespace ConsoleEShopMultilayered.BLL
{
    /// <summary>
    /// Interface for controller that incapsulate all BLL layer
    /// </summary> 
    public interface IController
    {
        /// <summary>
        /// Controller for requests about users
        /// </summary> 
         UserController UserController { get; }

        /// <summary>
        /// Controller for requests about products
        /// </summary> 
         ProductController ProductController { get; }

        /// <summary>
        /// Controller for requests about orders
        /// </summary> 
         OrderController OrderController { get; }

    }
}

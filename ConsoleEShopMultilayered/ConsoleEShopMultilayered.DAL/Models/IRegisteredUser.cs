namespace ConsoleEShopMultilayered.DAL.Models

{

    /// <summary>
    /// Interface of model for all registered users
    /// </summary>  
    public interface IRegisteredUser
    {
        /// <summary>
        /// Id of user
        /// </summary>  
        int Id { get; set; }

        /// <summary>
        /// username, login
        /// </summary>  
        string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>  
        string Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>  
        string Email { get; set; }
    }
}

namespace ConsoleEShopMultilayered.DAL.Models
{
    /// <summary>
    /// Id of user
    /// </summary>  
    public class RegisteredUser : IRegisteredUser
    {
        /// <summary>
        /// Id of user
        /// </summary>  
        public int Id { get; set; }

        /// <summary>
        /// username, login
        /// </summary>  
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>  
        public string Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>  
        public string Email { get; set; }
    }
}

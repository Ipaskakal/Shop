
namespace ConsoleEShopMultilayered.DAL.Models
{
    /// <summary>
    /// Model for administrator role
    /// </summary>  
    public class Administrator : RegisteredUser
    {
        /// <summary>
        /// name + surname
        /// </summary>  
        public string FullName { get; set; } = "";
    }
}

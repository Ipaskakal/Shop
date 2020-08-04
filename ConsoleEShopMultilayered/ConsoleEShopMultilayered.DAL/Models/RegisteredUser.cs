namespace ConsoleEShopMultilayered.DAL.Models
{
    public class RegisteredUser : IRegisteredUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

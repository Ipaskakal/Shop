namespace ConsoleEShopMultilayered.DAL.Models
{
    public interface IRegisteredUser
    {
        int Id { get; set; }

        string UserName { get; set; }

        string Password { get; set; }

        string Email { get; set; }
    }
}

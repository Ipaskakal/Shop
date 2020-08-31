namespace ConsoleEShopMultilayered.BLL.VievModels
{
    /// <summary>
    /// model to sign up user
    /// </summary> 
    public class RegisterUserViewModel
    {
        /// <summary>
        /// email
        /// </summary> 
        public string Email { get; set; } = "";

        /// <summary>
        /// username
        /// </summary> 
        public string UserName { get; set; } = "";

        /// <summary>
        /// password
        /// </summary> 
        public string Password { get; set; } = "";

        /// <summary>
        /// confirm password
        /// </summary> 
        public string ConfirmPassword { get; set; } = "";
    }
}

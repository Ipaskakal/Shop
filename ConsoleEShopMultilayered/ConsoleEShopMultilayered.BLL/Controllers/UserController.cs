using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using ConsoleEShopMultilayered.BLL.VievModels;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.BLL.Controllers
{
    /// <summary>
    /// Controller for requests about users
    /// </summary> 
    public class UserController
    {
        readonly IUserRepository userRepository;

        /// <summary>
        /// Constructor that gets user repository
        /// </summary> 
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Try to add new user
        /// </summary> 
        public bool SignUpUser(RegisterUserViewModel user)
        {
            return userRepository.Add(Mapping.Mapper.Map<RegisterUserViewModel, RegisteredUser>(user));
        }

        /// <summary>
        /// try to find and check registered user
        /// </summary> 
        public IRegisteredUser SignInUser(RegisterUserViewModel user)
        {
            IRegisteredUser user2 = userRepository.FindItemByKey(user.Email);
            if (user2 != null && user2.Email == user.Email && user.Password == user2.Password)
                return user2;
            return null;
        }

        /// <summary>
        /// try to find registered user
        /// </summary> 
        public UsersViewModel GetUser(string email)
        {
            return Mapping.Mapper.Map<IRegisteredUser, UsersViewModel>(userRepository.FindItemByKey(email));
        }

        /// <summary>
        /// get all users
        /// </summary> 
        public List<UsersViewModel> GetListOfUsers()
        {
            return Mapping.Mapper.Map<List<IRegisteredUser>, List<UsersViewModel>>(userRepository.GetAll());
        }

        /// <summary>
        /// update user information
        /// </summary> 
        public IRegisteredUser UpdateUser(UsersViewModel user, UsersViewModel newUser)
        {
            return userRepository.Update(Mapping.Mapper.Map<UsersViewModel, IRegisteredUser>(user), Mapping.Mapper.Map<UsersViewModel, IRegisteredUser>(newUser));

        }
    }
}
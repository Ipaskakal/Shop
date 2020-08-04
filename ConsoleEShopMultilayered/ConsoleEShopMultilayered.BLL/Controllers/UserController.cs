using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using ConsoleEShopMultilayered.BLL.VievModels;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.BLL.Controllers
{
    public class UserController
    {
        readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool SignUpUser(RegisterUserViewModel user)
        {
            return userRepository.Add(Mapping.Mapper.Map<RegisterUserViewModel, RegisteredUser>(user));
        }

        public IRegisteredUser SignInUser(RegisterUserViewModel user)
        {
            IRegisteredUser user2 = userRepository.FindItemByKey(user.Email);
            if (user2 != null && user2.Email == user.Email && user.Password == user2.Password)
                return user2;
            return null;
        }

        public UsersViewModel GetUser(string email)
        {
            return Mapping.Mapper.Map<IRegisteredUser, UsersViewModel>(userRepository.FindItemByKey(email));
        }

        public List<UsersViewModel> GetListOfUsers()
        {
            return Mapping.Mapper.Map<List<IRegisteredUser>, List<UsersViewModel>>(userRepository.GetAll());
        }

        public IRegisteredUser UpdateUser(UsersViewModel user, UsersViewModel newUser)
        {
            return userRepository.Update(Mapping.Mapper.Map<UsersViewModel, IRegisteredUser>(user), Mapping.Mapper.Map<UsersViewModel, IRegisteredUser>(newUser));

        }
    }
}
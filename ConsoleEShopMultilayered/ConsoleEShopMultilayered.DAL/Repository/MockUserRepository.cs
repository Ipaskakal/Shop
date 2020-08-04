using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.DAL.Repository.IRepository;
using Moq;
using System.Collections.Generic;

namespace ConsoleEShopMultilayered.DAL.Repository
{
    class MockUserRepository
    {
        static readonly List<IRegisteredUser> users = new List<IRegisteredUser>
        {
            new Administrator() { Id =1, UserName="Admin", Password="Admin", Email="Admin@gmail.com",FullName="Maxum Domalchuk"},
            new RegisteredUser() { Id=2, UserName="Max", Password="123", Email="ipaskakal@gmail.com"},
            new RegisteredUser() { Id=3, UserName="test12", Password="123456", Email="test@gmail.com"}
        };

        public IUserRepository UserRepository { get; set; }
        public MockUserRepository()
        {

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(mq => mq.Add(It.IsAny<IRegisteredUser>()))
                .Returns<IRegisteredUser>(user =>
                {
                    foreach (var tempUser in users)
                        if (user.Email == tempUser.Email) return false;
                    user.Id = users[users.Count - 1].Id + 1;
                    users.Add(user);
                    return true;
                });
            userRepositoryMock.Setup(mq => mq.FindItemByKey(It.IsAny<string>()))
                .Returns<string>(user =>
                {
                    foreach (var tempUser in users)
                        if (user == tempUser.Email) return tempUser;
                    return null;
                }
                );
            userRepositoryMock.Setup(mq => mq.FindItemById(It.IsAny<int>()))
                .Returns<int>(userId =>
                {
                    foreach (var tempUser in users)
                        if (userId == tempUser.Id) return tempUser;
                    return null;
                }
                );
            userRepositoryMock.Setup(mq => mq.Update(It.IsAny<IRegisteredUser>(), It.IsAny<IRegisteredUser>()))
                .Returns<IRegisteredUser, IRegisteredUser>((user, newUser) =>
                {
                    var userToChange = UserRepository.FindItemByKey(user.Email);
                    var checkEmail = UserRepository.FindItemByKey(newUser.Email);
                    if (userToChange != null && (checkEmail == null || checkEmail.Email.Equals(userToChange.Email)))
                    {
                        userToChange.Email = newUser.Email;
                        userToChange.Password = newUser.Password;
                        userToChange.UserName = newUser.UserName;
                        return userToChange;
                    }
                    else
                        return null;
                });
            userRepositoryMock.Setup(mq => mq.GetAll()).Returns(users);
            this.UserRepository = userRepositoryMock.Object;
        }


    }
}

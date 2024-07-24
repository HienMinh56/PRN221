using BOs.Entities;
using Repos;
using Repos.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository = null;
        public UserService()
        {
            if (userRepository == null)
            {
                userRepository = new UserRepository();
            }
        }

        public List<User> GetUsers()
        {
            return userRepository.GetUsers();
        }

        public User Login(string username, string password)
        {
            return userRepository.GetUser(username, password);
        }

        public async Task Add(User user)
        {
            await userRepository.Add(user);
        }

        public async Task Remove(string userId)
        {
            await userRepository.Remove(userId);
        }

        public async Task Update(string UserId, User user)
        {
            await userRepository.Update(UserId, user);
        }

        public User GetUserById(string UserId)
        {
            return userRepository.getUserByid(UserId);
        }

    }
}

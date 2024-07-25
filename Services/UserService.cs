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
        private readonly IUserRepository _userRepository = null;
        public UserService()
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository();
            }
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User Login(string username, string password)
        {
            return _userRepository.GetUser(username, password);
        }

        public async Task AddUser(User user)
        {
            bool userExists = await _userRepository.UserExists(user.UserName, user.Phone, user.Email);
            if (userExists)
            {
                throw new Exception("User already exists");
            }

            var maxUserIdStr = await _userRepository.GetMaxUserIdAsync();
            int maxUserId = 0;
            if (maxUserIdStr != null)
            {
                maxUserId = int.Parse(maxUserIdStr);
            }

            user.UserId = $"BAMEM{(maxUserId + 1).ToString().PadLeft(4, '0')}";

            await _userRepository.AddUser(user);
        }

        public async Task Remove(string userId)
        {
            await _userRepository.Remove(userId);
        }

        public async Task UpdateUser(string userId, User user)
        {
            var existingUser =  _userRepository.getUserByid(userId);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            bool emailExists = await _userRepository.UserExists(user.UserName, user.Phone, user.Email, userId);
            if (emailExists && existingUser.Email != user.Email)
            {
                throw new Exception("Email already exists");
            }

            bool phoneExists = await _userRepository.UserExists(user.UserName, user.Phone, user.Email,userId);
            if (phoneExists && existingUser.Phone != user.Phone)
            {
                throw new Exception("Phone number already exists");
            }

            existingUser.FullName = user.FullName;
            existingUser.Password = user.Password;
            existingUser.Phone = user.Phone;
            existingUser.Address = user.Address;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            existingUser.Status = user.Status;

            await _userRepository.UpdateUser(existingUser);
        }

        public User GetUserById(string UserId)
        {
            return _userRepository.getUserByid(UserId);
        }

    }
}

using BOs.Entities;
using DAOs;
using Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDAO = null;
        public UserRepository()
        {
            if(_userDAO == null)
            {
                _userDAO = new UserDAO();
            }
        }

        public User GetUser(string username, string password)
        {
            return _userDAO.GetUser(username, password);
        }

        public List<User> GetUsers()
        {
            return _userDAO.GetUsers();
        }

        public async Task AddUser(User user)
        {
            await _userDAO.AddUser(user);
        }

        public async Task UpdateUser(User user)
        {
            await _userDAO.UpdateUser(user);
        }
        public async Task<bool> UserExists(string username, string phone, string email, string userid =null)
        {
            return await _userDAO.UserExists(username, phone, email, userid);
        }

        public async Task<string> GetMaxUserIdAsync()
        {
            return await _userDAO.GetMaxUserIdAsync();
        }
        public async Task Remove(string userId)
        {
            await _userDAO.RemoveUser(userId);
        }

        public User getUserByid(string UserId)
        {
            return _userDAO.getUserByid(UserId);
        }

        
    }
}

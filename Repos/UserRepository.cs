using BOs.Entities;
using DAOs;
using Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Add(User user)
        {
             _userDAO.AddUser(user);
        }

        public void Update(User user)
        {
            _userDAO.UpdateUser(user);
        }

        public void Remove(string userId)
        {
            _userDAO.RemoveUser(userId);
        }

        
    }
}

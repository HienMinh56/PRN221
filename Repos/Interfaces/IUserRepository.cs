using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Interfaces
{
    public interface IUserRepository
    {
        User getUserByid(string UserId);
        List<User> GetUsers();
        User GetUser(string username, string password);
        public Task AddUser(User user);
        public Task UpdateUser(User user);
        public Task<bool> UserExists(string username, string phone, string email, string userId = null);
        public Task<string> GetMaxUserIdAsync();
        Task Remove(string userId);
    }
}

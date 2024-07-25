using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(string UserId);
        User Login(string username, string password); 
        List<User> GetUsers();
        public Task AddUser(User user);
        public Task UpdateUser(string userId, User user);
        Task Remove(string userId);

    }
}

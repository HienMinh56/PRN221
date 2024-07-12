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
        Task Add(User user);
        Task Update(string UserId, User user);
        Task Remove(string userId);
    }
}

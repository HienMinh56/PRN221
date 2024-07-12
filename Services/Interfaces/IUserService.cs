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
        User getUserByid(string UserId);
        User Login(string username, string password); 
        List<User> GetUsers();
        Task Add(User user);
        Task Update(string UserId, User user);
        Task Remove(string userId);

    }
}

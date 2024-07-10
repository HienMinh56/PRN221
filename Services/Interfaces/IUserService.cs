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
        User Login(string username, string password); 
        List<User> GetUsers();
        void Add(User user);
        void Update(User user);
        void Remove(string userId);

    }
}

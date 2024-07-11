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
        List<User> GetUsers();
        User GetUser(string username, string password);
        void Add(User user);
        void Update(User user);
        void Remove(string userId);

    }
}

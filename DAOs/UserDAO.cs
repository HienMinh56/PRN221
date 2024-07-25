using BCrypt.Net;
using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DAOs
{
    public class UserDAO
    {
        private readonly Dbprn221Context _context;
        private static UserDAO instance = null;

        public UserDAO()
        {
             _context = new Dbprn221Context();
        }

        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    {
                    instance = new UserDAO();
                    }
                }
                return instance;
            }
        }

        public List<User> GetUsers()
        {
            return _context.Users.OrderByDescending(u=>u.UserId).ToList();
        }

        public User getUserByid(string UserId)
        {
            return  _context.Users.OrderByDescending(u => u.UserId).FirstOrDefault(u => u.UserId == UserId);
        }

        public User GetUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username&&u.Status==1);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UserExists(string userName, string phone, string email, string excludeUserId = null)
        {
            var user = await _context.Users
                .Where(u => (u.Email == email || u.Phone == phone) && u.UserId != excludeUserId)
                .FirstOrDefaultAsync();

            return user != null;
        }

        public async Task<string> GetMaxUserIdAsync()
        {
            return await _context.Users
                .Where(u => u.UserId.StartsWith("BAMEM"))
                .Select(u => u.UserId.Substring(5))
                .OrderByDescending(id => id)
                .FirstOrDefaultAsync();
        }
        public async Task RemoveUser(string userId)
        {
            try
            {
                var user = getUserByid(userId);
                if (user != null)
                {
                    user.Status = 0;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }

            } catch(Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}

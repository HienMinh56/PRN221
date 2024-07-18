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
            return  _context.Users.FirstOrDefault(u => u.UserId == UserId);
        }

        public User GetUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }

        public async Task AddUser(User user)
        {
            var userIsExisted = _context.Users.Any(u => u.UserName == user.UserName || u.Phone == user.Phone || u.Email == user.Email);
            if (!userIsExisted)
            {
                // Lấy giá trị UserId lớn nhất hiện tại từ cơ sở dữ liệu
                var maxUserIdStr = _context.Users
                    .Where(u => u.UserId.StartsWith("BAMEM"))
                    .Select(u => u.UserId.Substring(5))
                    .OrderByDescending(id => id)
                    .FirstOrDefault();

                int maxUserId = 0;
                if (maxUserIdStr != null)
                {
                    maxUserId = int.Parse(maxUserIdStr);
                }

                // Tạo UserId mới theo định dạng BAMEMxxxx
                user.UserId = $"BAMEM{(maxUserId + 1).ToString().PadLeft(4, '0')}";

                // Thêm người dùng mới vào cơ sở dữ liệu
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("The user already exists");
            }
        }


        public async Task UpdateUser(string UserId, User user)
        {
            User existingUser = getUserByid(UserId);
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                existingUser.Password = user.Password;
                existingUser.Phone = user.Phone;
                existingUser.Address = user.Address;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;
                existingUser.Status = user.Status;
                _context.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User Not Found");
            }
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

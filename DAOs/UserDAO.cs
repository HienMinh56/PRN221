using BOs;
using BOs.Entities;

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
            return _context.Users.ToList();
        }

        

        public User GetUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

        public void AddUser(User user)
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
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("The user already exists");
            }
        }


        public void UpdateUser(User user)
        {
            User existingUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                existingUser.Password = user.Password;
                existingUser.Phone = user.Phone;
                existingUser.Address = user.Address;
                existingUser.Email = user.Email;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

        public void RemoveUser(string userId)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    user.Status = 1;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }

            } catch(Exception e) {
                throw new Exception(e.Message);
            }
        }

        

    }
}

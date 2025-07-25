using BulkyWeb.Models;

namespace BulkyWeb.Service
{
    public class UserStoreService
    {
        private readonly List<Register> _users = new();

        public UserStoreService()
        {
            // Add static Admin
            _users.Add(new Register
            {
                Name = "Admin User",
                Email = "admin@example.com",
                Mobile = "1234567890",
                Dob = new DateTime(1990, 1, 1),
                Username = "admin",
                Password = "admin123",
                Role = "Admin"
            });

            // Add static User
            _users.Add(new Register
            {
                Name = "Normal User",
                Email = "user@example.com",
                Mobile = "9876543210",
                Dob = new DateTime(1995, 5, 15),
                Username = "user",
                Password = "user123",
                Role = "User"
            });
        }

        public void AddUser(Register user)
        {
            if (!_users.Any(u => u.Username == user.Username))
                _users.Add(user);
        }

        public Register GetUser(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public List<Register> GetUsersByRole(string role)
        {
            return _users
                .Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Register> GetAllUsers()
        {
            return _users;
        }
    }
}

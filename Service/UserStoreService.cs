using BulkyWeb.Models;

namespace BulkyWeb.Service
{
    public class UserStoreService
    {
        private readonly List<Register> _users = new();

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
            return _users.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Register> GetAllUsers()
        {
            return _users;
        }
           
    }
}

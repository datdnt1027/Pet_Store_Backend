using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pet_store_backend.infrastructure.Persistence
{
    internal class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();
        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }
    }
}

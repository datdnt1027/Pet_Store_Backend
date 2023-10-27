using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Entities;
using pet_store_backend.domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pet_store_backend.infrastructure.Persistence.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void Add(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }

        public User? GetUserByEmail(string email)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Email == email);
        }

        // private static readonly List<User> _users = new List<User>();
        // public void Add(User user)
        // {
        //     _users.Add(user);
        // }

        // public User? GetUserByEmail(string email)
        // {
        //     return _users.SingleOrDefault(u => u.Email == email);
        // }
    }
}

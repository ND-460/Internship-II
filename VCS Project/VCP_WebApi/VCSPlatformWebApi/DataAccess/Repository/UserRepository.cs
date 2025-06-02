using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public class UserRepository
    {
        private readonly VCSDbContext _vcSDbContext;
        public UserRepository(VCSDbContext vcSDbContext)
        {
            _vcSDbContext = vcSDbContext;
        }

        public List<User> GetUsers()
        {
            return _vcSDbContext.Users.ToList();
        }
        public User? Login(string username, string password)
        {
            var user = _vcSDbContext.Users.FirstOrDefault(u => u.EmailAddress == username && u.Password == password);
            return user;
        }
    }
}

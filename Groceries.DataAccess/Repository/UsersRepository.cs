using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groceries.DataAccess.Models;

namespace Groceries.DataAccess.Repository
{
    public class UsersRepository
    {
        private readonly GroceriesDbContext _context;
        public UsersRepository(GroceriesDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll() 
        {
            List<User> users = _context.Users.ToList();
            return users;
        }
        public void AddUser(User user) 
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User? Login(string username, string password) 
        {
            var user = _context.Users.Where(x => x.Email == username && x.Password == password).FirstOrDefault();
            return user;
        }
    }
}

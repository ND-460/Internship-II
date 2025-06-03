using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groceries.DataAccess.DtoHelper;
using Groceries.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

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
        public List<User> GetAllUsers1(FilterVM filterVM)
        {
            var query = _context.Users.AsQueryable();

            // Search filter
            if (!string.IsNullOrWhiteSpace(filterVM.SearchFilter))
            {
                query = query.Where(u => u.Name.ToLower().Contains(filterVM.SearchFilter.ToLower()));
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(filterVM.SortBy))
            {
                if (filterVM.SortBy.ToLower() == "name")
                {
                    query = filterVM.SortDirection == "asc" ? query.OrderBy(u => u.Name) : query.OrderByDescending(u => u.Name);
                }
                else if (filterVM.SortBy.ToLower() == "email")
                {
                    query = filterVM.SortDirection == "asc" ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email);
                }
            }

            // Pagination
            query = query.Skip((filterVM.PageNumber - 1) * filterVM.PageSize).Take(filterVM.PageSize);

            // Include groceries
            query = query.Include(u => u.Groceries);

            return query.ToList();
        }

    }
}

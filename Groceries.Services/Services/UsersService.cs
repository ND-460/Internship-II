using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groceries.DataAccess.DtoHelper;
using Groceries.DataAccess.Models;
using Groceries.DataAccess.Repository;

namespace Groceries.Services.Services
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository;
        public UsersService(UsersRepository usersRepository) 
        {
            _usersRepository = usersRepository;
        }
        public List<User> GetAll() 
        {
            return _usersRepository.GetAll();
        }
        public void AddUser(User user) 
        {
            _usersRepository.AddUser(user);
        }
        public User? Login(string username, string password) 
        {
            return _usersRepository.Login(username, password);
        }
        public List<UserVM> GetAllUsersNew(FilterVM filterVM)
        {
            var users = _usersRepository.GetAllUsers1(filterVM);

            return users.Select(u => new UserVM()
            {
                Email = u.Email,
                Id = u.Id,
                Name = u.Name,
                Role = u.Role,
                Groceries = u.Groceries.Select(u => new Grocery()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Category = u.Category,
                    Price = u.Price
                }).ToList()
            }).ToList();
        }

    }
}

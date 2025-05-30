using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}

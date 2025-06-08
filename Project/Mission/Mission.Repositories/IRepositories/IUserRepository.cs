using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<string> DeleteUser(int id);
        Task<UserResponseModel> GetUserById(int id);

        Task<List<UserResponseModel>> GetAllUsers();
    }
}

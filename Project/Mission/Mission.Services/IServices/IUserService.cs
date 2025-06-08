using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission.Entities.Models;

namespace Mission.Services.IServices
{
    public interface IUserService
    {
        Task<UserResponseModel> GetUserById(int id);
        Task<string> DeleteUser(int id);
        Task<List<UserResponseModel>> GetAllUsers();
    }
}

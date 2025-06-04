using Microsoft.EntityFrameworkCore.Update.Internal;
using Mission.Entities;
using Mission.Entities.Dto;
using Mission.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.IRepositories
{
    public interface ILoginRepository
    {
        LoginUserResponseModel LoginUser(LoginUserRequestModel model);

        Task<string> Register(RegisterUserModel model);

        List<User> GetUsersById(int id);

        User LoginUserDetailById(int id);

        User GetUser(int id);

        User UpdateUser(User user);
    }
}

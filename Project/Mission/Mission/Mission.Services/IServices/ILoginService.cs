using Mission.Entities;
using Mission.Entities.Dto;
using Mission.Entities.Models;
namespace Mission.Services.IServices
{
    public interface ILoginService
    {
        ResponseResult LoginUser(LoginUserRequestModel model);

        LoginUserResponseModel UserLogin(LoginUserRequestModel model);

        Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest);
        UserResponseModel LoginUserDetailById(int id);
        Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel);
        User UpdateUser(UpdateUserDto updateUserDto);

        AddUserDetailsRequestModel GetUserProfileDetailById(int id);
    }
}

using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using VCSPlatformWebApi.Dto;

namespace VCSPlatformWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserServices _userServices;
        public LoginController(UserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("GetAllUsers")]
        public ActionResult<List<User>> GetUser()
        {
            return Ok(_userServices.GetUsers());
        }
        [HttpPost("/Login")]
        public ActionResult Login([FromBody] LoginReqDto dto)
        {
            var user = _userServices.Login(dto.EmailAddress,dto.Password);
            if(user == null)
            {
                return BadRequest("Please Enter correct password and email");
            }
            return Ok(new LoginResDto() { EmailAddress = user.EmailAddress, FirstName = user.FirstName, LastName = user.LastName,UserType = user.UserType });
        }
    }
}

using Groceries.DataAccess.DtoHelper;
using Groceries.DataAccess.Models;
using Groceries.Services.Services;
using GroceriesManagement.Dto;
using GroceriesManagement.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GroceriesManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly JwtHelper _jwtHelper;
        public UsersController(UsersService usersService,JwtHelper jwthelper) 
        {
            _usersService = usersService;
            _jwtHelper = jwthelper;
        }

        [HttpGet("/GetAll")]
        public ActionResult<List<User>> GetAll() 
        {
            return Ok(_usersService.GetAll());
        }
        [HttpPost("/AddUser")]
        public ActionResult AddUser(User user) 
        {
            _usersService.AddUser(user);
            return Ok("User Added Successfully");
        }
        [HttpPost("/Login")]
        public ActionResult Login([FromBody] LoginReqDto dto) 
        {
            var user = _usersService.Login(dto.Email, dto.Password);
            if (user == null) 
            {
                return NotFound("Please check your email and password");
            }

            var token = _jwtHelper.GetJwtToken(user);
            return Ok(new LoginResDto() { Email = user.Email, Name = user.Name, Role = user.Role, Token = token});
            
        }
        [HttpPost("/GetAllUserNew")]
        public ActionResult<List<UserVM>> GetAllUserNew([FromBody] FilterVM filterVM)
        {
            var data = _usersService.GetAllUsersNew(filterVM);
            return Ok(data);
        }
    }
}

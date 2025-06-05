using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities.Dto;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginService loginService, IWebHostEnvironment hostingEnvironment) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;
        private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
        ResponseResult result = new ResponseResult();

        [HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(LoginUserRequestModel model)
        {
            try
            {
                result.Data = _loginService.LoginUser(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterUserModel model)
        {
            try
            {
                var res = await _loginService.Register(model);
                return Ok(new ResponseResult() { Data = "User Added !", Result = ResponseStatus.Success, Message = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add records" });
            }
        }
        [HttpGet]
        [Route("GetUserById")]
        public ActionResult<List<User>> GetUsersById(int id)
        {
            try
            {
                var user = _loginService.GetUsersById(id);
                return Ok(user);
            }
            catch
            {
                return NotFound("User not found");
            }
        }
        [HttpGet]
        [Route("LoginUserDetailById/{id}")]
        public ActionResult<User> LoginUserDetailById(int id)
        {
            try
            {
                var user = _loginService.LoginUserDetailById(id);
                return Ok(user);
            }
            catch
            {
                return NotFound("Login User not found");
            }
        }
        [HttpPost]
        [Route("UpdateUser")]
        public ActionResult UpdateUser([FromForm] UpdateUserDto userData)
        {
            try
            {
                var updatedUser = _loginService.UpdateUser(userData);
                return Ok(new ResponseResult
                {
                    Data = updatedUser,
                    Result = ResponseStatus.Success,
                    Message = "User updated successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult
                {
                    Data = null,
                    Result = ResponseStatus.Error,
                    Message = ex.Message
                });
            }
        }

    }
}

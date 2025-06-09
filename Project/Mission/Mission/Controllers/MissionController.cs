using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;
using Mission.Services.Services;

namespace Mission.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionController(IMissionService missionService) : Controller
    {
        //[HttpGet]
        //[Route("MissionList")]
        //public ResponseResult MissionList()
        //{
        //    return new ResponseResult() { Data = missionService.GetMissionList(), Message = "", Result = ResponseStatus.Success };
        //}

        [HttpPost]
        [Route("AddMission")]
        public ActionResult AddMission(AddMissionRequestModel model)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                var data = missionService.AddMission(model);
                result.Data = data;
                result.Message = "Success";
                result.Result = ResponseStatus.Success;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
                return BadRequest(result);
            }
        }
        [HttpGet]
        [Route("MissionList")]
        public async Task<IActionResult> GetAllMissionAsync()
        {
            var response = await missionService.GetAllMissionAsync();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpGet]
        [Route("MissionDetailById/{id:int}")]
        public async Task<IActionResult> GetMissionById(int id)
        {
            var response = await missionService.GetMissionById(id);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }
    }
}

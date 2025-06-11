using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController(IMissionService missionService) : ControllerBase
    {
        private readonly IMissionService _missionService = missionService;

        [HttpPost]
        [Route("AddMission")]
        public IActionResult AddMission(MissionRequestViewModel model)
        {
            var response = _missionService.AddMission(model);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpPost]
        [Route("UpdateMission")]
        public IActionResult UpdateMission(UpdateMissionRequestModel model)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                var response = _missionService.UpdateMission(model);
                return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("DeleteMission/{id}")]
        public IActionResult DeleteMission(int id)
        {
            try
            {
                var result = missionService.DeleteMission(id);
                return Ok("Deleted Successfully");
            }

            catch (Exception ex)
            {
                return BadRequest("Error in deleting mission");
            }
        }

        [HttpGet]
        [Route("MissionList")]
        public async Task<IActionResult> GetAllMissionAsync()
        {
            var response = await _missionService.GetAllMissionAsync();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpGet]
        [Route("MissionDetailById/{id:int}")]
        public async Task<IActionResult> GetMissionById(int id)
        {
            var response = await _missionService.GetMissionById(id);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpGet]
        [Route("MissionApplicationList")]
        public IActionResult MissionApplicationList()
        {
            var response = _missionService.GetMissionApplicationList();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpPost]
        [Route("MissionApplicationApprove")]
        public async Task<IActionResult> MissionApplicationApprove(UpdateMissionApplicationModel missionApp)
        {
            try
            {
                var ret = await _missionService.MissionApplicationApprove(missionApp);
                return Ok(new ResponseResult() { Data = ret, Message = string.Empty, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Message = ex.Message, Result = ResponseStatus.Error });
            }
        }
        [HttpPost]
        [Route("MissionApplicationDelete")]
        public async Task<IActionResult> MissionApplicationDelete(UpdateMissionApplicationModel missionApp)
        {
            try
            {
                var ret = await _missionService.MissionApplicationDelete(missionApp);
                return Ok(new ResponseResult() { Data = ret, Message = string.Empty, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Message = ex.Message, Result = ResponseStatus.Error });
            }
        }
    }
}

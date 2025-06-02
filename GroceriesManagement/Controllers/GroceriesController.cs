//using GroceriesManagement.Services;
using Groceries.Services.Services;
using Groceries.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
//using GroceriesManagement.Models;
namespace GroceriesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroceriesController : ControllerBase
    {
        private readonly GroceriesService _groceryservice;
        public GroceriesController(GroceriesService groceryservice)
        {
            _groceryservice = groceryservice;
        }

        [HttpGet("GetAllGroceries")]
        [Authorize(Roles ="admin,manager")]
        public ActionResult<List<Grocery>> GetAllGroceries()
        {
            List<Grocery> groceries = _groceryservice.GetAllGrocery();
            if (groceries == null || groceries.Count == 0)
            {
                return NotFound("No groceries found");
            }
            return Ok(groceries);
        }
        [HttpGet("GetParticularGrocery")]
        public ActionResult<List<Grocery>> GetParticularGrocery(int id)
        {
            Grocery grocery = _groceryservice.GetGrocery(id);
            if (grocery == null)
                return NotFound("No grocery with the id: " + id);
            return Ok(grocery);
        }
        [HttpPost("AddGrocery")]
        [Authorize(Roles ="admin")]
        public ActionResult AddGrocery(Grocery grocery)
        {
            _groceryservice.AddGrocery(grocery);
            return Ok("Grocery successfully added");
        }
        [HttpPut("UpdateGrocery")]
        public ActionResult UpdateGrocery(Grocery grocery)
        {
            int updateStatus = _groceryservice.UpdateGrocery(grocery);
            if (updateStatus == -1)
                return NotFound("Grocery nor found");
            else if (updateStatus == 1)
                return Ok("Grocery successfully updated");
            else
                return BadRequest("Error");
        }
        [HttpDelete("DeleteGrocery")]
        public ActionResult DeleteGrocery(int id)
        {
            int deleteStatus = _groceryservice.DeleteGrocery(id);
            if (deleteStatus == -1)
                return NotFound("Grocery not found");
            else if (deleteStatus == 1)
                return Ok("Grocery succesfully deleted");
            else
                return BadRequest("Error");
        }
        [HttpGet("GetFilteredGroceryByCategory")]
        public ActionResult<List<Grocery>>GetFilteredCategory(string category) 
        {
            List<Grocery> groceries = _groceryservice.GetFilteredCategory(category);
            if (groceries == null || groceries.Count == 0)
                return NotFound("No grocery found for this category");
            else
                return Ok(groceries);
        }
    }
}

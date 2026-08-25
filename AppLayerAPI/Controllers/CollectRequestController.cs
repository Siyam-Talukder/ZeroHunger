using BLL.Models;
using BLL.Services;
using DAL.EF.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectRequestController : ControllerBase
    {
        CollectRequestService service;
        public CollectRequestController(CollectRequestService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = service.GetById(id);
            return Ok(data);
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            var data = service.All();
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = service.Delete(id);
            return Ok("Deleted");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CollectRequestModel r)
        {
            var res = service.Update(id, r);
            return Ok("Updated");
        }

        [HttpPost]
        public IActionResult Create(CollectRequestModel request)
        {
            var isSuccess = service.Create(request);
            if (isSuccess)
            {
                return Ok(new { Message = "Collect request created successfully." });
            }

            return BadRequest(new { Message = "Failed to create request. Make sure MaxPreservationTime is in the future." });
        }

        //Resturant will assign
        [HttpPut("assign/{requestId}")]
        public IActionResult Assign(int requestId, [FromBody] EmployeeInfoModel model)
        {
            var isSuccess = service.AssignEmployee(requestId, model.EmployeeId);

            if (isSuccess) return Ok(new { Message = "Employee assigned successfully." });
            return BadRequest(new { Message = "Failed to assign." });
        }


        //employee can check assigned task
        [HttpGet("{employeeId}/assigned")]
        public IActionResult GetAssignedTasks(int employeeId)
        {
            var tasks = service.GetAssignedTasks(employeeId);
            if (tasks.Count == 0)
            {
                return Ok(new { Message = "You have no assigned tasks." });
            }

            return Ok(tasks);
        }



        //employee can accept/ cancel tasks
        [HttpPut("accept/{requestId}")]
        public IActionResult Accept(int requestId, [FromBody] EmployeeInfoModel model)
        {
            var isSuccess = service.AcceptTask(requestId, model.EmployeeId);
            if (isSuccess) return Ok(new { Message = "Task accepted! Proceed to the restaurant." });
            return BadRequest(new { Message = "Failed to accept task." });
        }

        [HttpPut("cancel/{requestId}")]
        public IActionResult Cancel(int requestId, [FromBody] EmployeeInfoModel model)
        {
            var isSuccess = service.CancelTask(requestId, model.EmployeeId);
            if (isSuccess) return Ok(new { Message = "Task cancelled. It has been returned to the pending pool." });
            return BadRequest(new { Message = "Failed to cancel task." });
        }


        //If accepts, then he can Collect.
        [HttpPut("collect/{requestId}")]
        public IActionResult CollectFood(int requestId, [FromBody] EmployeeInfoModel model)
        {
            var result = service.CollectFood(requestId, model.EmployeeId);

            if (result == "Success") return Ok(new { Message = "Food successfully collected from the restaurant." });
            if (result == "Expired") return BadRequest(new { Message = "Too late! The food has expired." });

            return BadRequest(new { Message = "Failed to collect. Invalid request or not assigned to you." });
        }


        //If picked up, then he can complete.
        [HttpPut("complete/{requestId}")]
        public IActionResult Complete(int requestId, [FromBody] EmployeeInfoModel model)
        {
            var result = service.CompleteRequest(requestId, model.EmployeeId);

            if (result == "Success")
            {
                return Ok(new { Message = "Request completed successfully. Food was delivered on time!" });
            }
            else if (result == "Expired")
            {
                return BadRequest(new { Message = "Food expired!" });
            }

            return BadRequest(new { Message = "Failed to complete. Request might not exist, or it is not assigned to you." });
        }
    }
}

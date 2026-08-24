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

        [HttpPut("{requestId}/assign/{employeeId}")]
        public IActionResult Assign(int requestId, int employeeId)
        {
            var isSuccess = service.AssignEmployee(requestId, employeeId);
            if (isSuccess)
            {
                return Ok(new { Message = "Employee assigned successfully." });
            }
            return BadRequest(new { Message = "Failed to assign. Request might not exist, or it is not in 'Pending' status." });
        }

        [HttpPut("{requestId}/complete/{employeeId}")]
        public IActionResult Complete(int requestId, int employeeId)
        {
            var result = service.CompleteRequest(requestId, employeeId);

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

        [HttpPut("{requestId}/accept/{employeeId}")]
        public IActionResult Accept(int requestId, int employeeId)
        {
            var isSuccess = service.AcceptTask(requestId, employeeId);
            if (isSuccess) return Ok(new { Message = "Task accepted! Proceed to the restaurant." });
            return BadRequest(new { Message = "Failed to accept task." });
        }

        [HttpPut("{requestId}/cancel/{employeeId}")]
        public IActionResult Cancel(int requestId, int employeeId)
        {
            var isSuccess = service.CancelTask(requestId, employeeId);
            if (isSuccess) return Ok(new { Message = "Task cancelled. It has been returned to the pending pool." });
            return BadRequest(new { Message = "Failed to cancel task." });
        }

        [HttpPut("{requestId}/pickup/{employeeId}")]
        public IActionResult PickUpFood(int requestId, int employeeId)
        {
            var result = service.CollectFood(requestId, employeeId);

            if (result == "Success") return Ok(new { Message = "Food successfully collected from the restaurant." });
            if (result == "Expired") return BadRequest(new { Message = "Too late! The food has expired." });

            return BadRequest(new { Message = "Failed to collect. Invalid request or not assigned to you." });
        }

        [HttpGet("employee/{employeeId}/assigned")]
        public IActionResult GetAssignedTasks(int employeeId)
        {
            var tasks = service.GetAssignedTasks(employeeId);
            if (tasks.Count == 0)
            {
                return Ok(new { Message = "You have no pending assignments at the moment." });
            }

            return Ok(tasks);
        }

    }
}

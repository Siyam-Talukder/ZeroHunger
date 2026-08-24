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
            return Ok("Updated1");
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
            var isSuccess = service.CompleteRequest(requestId, employeeId);
            if (isSuccess)
            {
                return Ok(new { Message = "Request completed successfully." });
            }

            return BadRequest(new { Message = "Failed to complete. Request might not exist, or it is not in 'Assigned' status." });
        }
    }
}

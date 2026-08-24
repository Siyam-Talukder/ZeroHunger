using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResturantController : ControllerBase
    {
        ResturantService service;
        public ResturantController(ResturantService service)
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

        [HttpPost]
        public IActionResult Create(ResturantModel r)
        {
            var isSuccess = service.Create(r);
            if (isSuccess)
            {
                return Ok(new { Message = "Restaurant created successfully." });
            }

            return BadRequest(new { Message = "Failed to create restaurant." });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ResturantModel r)
        {
            var res = service.Update(id, r);
            return Ok("Updated");
        }
    }
}

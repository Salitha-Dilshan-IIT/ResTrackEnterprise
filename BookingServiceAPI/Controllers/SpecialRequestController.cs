using BookingServiceAPI.Dtos;
using BookingServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingServiceAPI.Controllers
{
    [ApiController]
    [Route("api/specialrequests")]
    public class SpecialRequestController : ControllerBase
    {
        private readonly SpecialRequestService _service;

        public SpecialRequestController(SpecialRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var r = _service.GetById(id);
            if (r == null) return NotFound();
            return Ok(r);
        }

        [HttpGet("booking/{bookingId}")]
        public IActionResult GetByBooking(int bookingId)
        {
            return Ok(_service.GetByBooking(bookingId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSpecialRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _service.Create(dto);
            return Ok(new { message = "Request added" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateSpecialRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _service.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }

}

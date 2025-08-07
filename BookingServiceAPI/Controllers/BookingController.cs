using BookingServiceAPI.Dtos;
using BookingServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _service;

        public BookingController(BookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var booking = _service.GetById(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateBookingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _service.Create(dto);
                return Ok(new { message = "Booking created successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateBookingDto dto)
        {
            try
            {
                _service.Update(id,dto);
                return Ok(new { message = "Booking update successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [HttpGet("weeklyreport")]
        [Produces("application/xml")]
        public async Task<IActionResult> GetWeeklyReport()
        {
            var report = await _service.GenerateWeeklyReportAsync();
            return Ok(report); 
        }

        [HttpGet("stats")]
        public ActionResult<BookingStatsDto> GetStats()
        {
            var stats = _service.GetStatistics();
            return Ok(stats);
        }

    }
}

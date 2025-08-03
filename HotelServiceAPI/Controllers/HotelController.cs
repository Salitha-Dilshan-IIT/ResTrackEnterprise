using Microsoft.AspNetCore.Mvc;
using HotelServiceAPI.Models;
using HotelServiceAPI.Services;

namespace HotelServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly HotelService _service;

        public HotelController(HotelService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var hotel = _service.GetById(id);
            if (hotel == null) return NotFound();
            return Ok(hotel);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Hotel hotel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _service.Create(hotel);
            return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Hotel hotel)
        {
            if (id != hotel.Id) return BadRequest();
            if (!_service.GetAll().Any(h => h.Id == id)) return NotFound();
            _service.Update(hotel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.GetAll().Any(h => h.Id == id)) return NotFound();
            _service.Delete(id);
            return NoContent();
        }
    }
}

using HotelServiceAPI.Dtos;
using HotelServiceAPI.Models;
using HotelServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            var hotels = _service.GetAll();

            var result = hotels.Select(h => new HotelDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                Description = h.Description
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var hotel = _service.GetById(id);
            if (hotel == null) return NotFound();

            var dto = new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                Description = hotel.Description
            };

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateHotelDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var hotel = new Hotel
            {
                Name = dto.HotelName,
                Address = dto.HotelAddress,
                Description = dto.HotelDescription
            };

            _service.Create(hotel);

            var result = new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                Description = hotel.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateHotelDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingHotel = _service.GetById(id);
            if (existingHotel == null)
                return NotFound();

            // Update entity fields with values from DTO
            existingHotel.Name = dto.HotelName;
            existingHotel.Address = dto.HotelAddress;
            existingHotel.Description = dto.HotelDescription;

            _service.Update(existingHotel);

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

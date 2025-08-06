using Microsoft.AspNetCore.Mvc;
using HotelServiceAPI.Services;
using HotelServiceAPI.Dtos;
using HotelServiceAPI.Models;

namespace HotelServiceAPI.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _service;

        public RoomController(RoomService service)
        {
            _service = service;
        }

        [HttpGet("hotel/{hotelId}")]
        public IActionResult GetByHotel(int hotelId)
        {
            var rooms = _service.GetRoomsByHotel(hotelId);

            var result = rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomType = r.RoomType,
                PricePerNight = r.PricePerNight,
                HotelId = r.HotelId,
                Description = r.Description
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = _service.GetRoom(id);
            if (room == null)
                return NotFound();

            var result = new RoomDto
            {
                Id = room.Id,
                RoomType = room.RoomType,
                PricePerNight = room.PricePerNight,
                HotelId = room.HotelId,
                Description = room.Description
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRoomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate hotel ID before creating room
            if (!_service.IsHotelValid(dto.HotelId))
                return BadRequest(new { message = "Invalid hotel ID." });

            var room = new Room
            {
                RoomType = dto.RoomType,
                PricePerNight = dto.PricePerNight,
                Description = dto.Description,
                HotelId = dto.HotelId
            };

            _service.CreateRoom(room);

            return Ok(new { message = "Room created successfully." });
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateRoomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var room = _service.GetRoom(id);
            if (room == null)
                return NotFound();

            room.RoomType = dto.RoomType;
            room.PricePerNight = dto.PricePerNight;
            room.Description = dto.Description;
            room.HotelId = dto.HotelId;

            _service.UpdateRoom(room);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = _service.GetRoom(id);
            if (room == null)
                return NotFound();

            _service.DeleteRoom(id);
            return NoContent();
        }
    }
}

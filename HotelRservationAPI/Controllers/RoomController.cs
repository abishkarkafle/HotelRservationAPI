using HotelRservationAPI.DTO;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelRservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        #region HTTPGet


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Room>))]

        public IActionResult GetBilling()
        {
            var billing = _roomRepository.GetRooms();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(billing);
        }

        [HttpGet("{LogInID}")]
        [ProducesResponseType(200, Type = typeof(Room))]
        [ProducesResponseType(400)]

        public IActionResult GetAccesslog(int LogInID)
        {
            if (!_roomRepository.RoomExist(LogInID))
            {
                return NotFound();
            }

            var AccessLog = _roomRepository.GetRoom(LogInID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(AccessLog);
        }

        #endregion

        #region Post
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRoom([FromBody] RoomDto  roomCreate)
        {
            if (roomCreate == null)
                return BadRequest(ModelState);

            var category = _roomRepository.GetRooms()
                .Where(c => c.RoomType.Trim().ToUpper() == roomCreate.RoomType.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = new Room
            {
                RoomType = roomCreate.RoomType
            };

            if (!_roomRepository.CreateRoom(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
        #endregion

        #region Update

        [HttpPut("{RoomID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCategory(int RoomID, [FromBody] RoomDto updatedRoom)
        {
            if (updatedRoom == null)
                return BadRequest(ModelState);

            if (RoomID != updatedRoom.RoomID)
                return BadRequest(ModelState);

            if (!_roomRepository.RoomExist(RoomID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = new Room
            {
                RoomID = updatedRoom.RoomID,
                RoomType = updatedRoom.RoomType,
                AvailableCount = updatedRoom.AvailableCount,
                PricePerNight = updatedRoom.PricePerNight
            };

            if (!_roomRepository.UpdateRoom(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{RoomID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRoom(int roomID)
        {
            if (!_roomRepository.RoomExist(roomID))
            {
                return NotFound();
            }

            var ReservationToDelete = _roomRepository.GetRoom(roomID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_roomRepository.DeleteRoom(ReservationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        #endregion
    }
}

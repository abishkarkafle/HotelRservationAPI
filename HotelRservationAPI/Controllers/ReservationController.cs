using HotelRservationAPI.DTO;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelRservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }


        #region HTTPGet


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]

        public IActionResult GetBilling()
        {
            var billing = _reservationRepository.GetReservations();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(billing);
        }

        [HttpGet("{LogInID}")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]

        public IActionResult GetAccesslog(int LogInID)
        {
            if (!_reservationRepository.ReservationExist(LogInID))
            {
                return NotFound();
            }

            var AccessLog = _reservationRepository.GetReservation(LogInID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(AccessLog);
        }

        #endregion

        #region Post
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReservation([FromBody] ReservationDto  reservationCreate)
        {
            if (reservationCreate == null)
                return BadRequest(ModelState);

            var category = _reservationRepository.GetReservations()
                .Where(c => c.RoomType.Trim().ToUpper() == reservationCreate.RoomType.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = new Reservation
            {
                RoomType = reservationCreate.RoomType
            };

            if (!_reservationRepository.CreateReservation(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
        #endregion


        #region Update

        [HttpPut("{ReservationID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCategory(int ReservationID, [FromBody] ReservationDto updatedReservation)
        {
            if (updatedReservation == null)
                return BadRequest(ModelState);

            if (ReservationID != updatedReservation.ReservationID)
                return BadRequest(ModelState);

            if (!_reservationRepository.ReservationExist(ReservationID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = new Reservation
            {
                ReservationID = updatedReservation.ReservationID,
                RoomType = updatedReservation.RoomType,
                CheckinDate = updatedReservation.CheckinDate,
                CheckoutDate = updatedReservation.CheckoutDate
            };

            if (!_reservationRepository.UpdateReservation(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{ReservationID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int ReservationID)
        {
            if (!_reservationRepository.ReservationExist(ReservationID))
            {
                return NotFound();
            }

            var ReservationToDelete = _reservationRepository.GetReservation(ReservationID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reservationRepository.DeleteReservation(ReservationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        #endregion
    }
}

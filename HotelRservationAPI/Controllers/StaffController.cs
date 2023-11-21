using HotelRservationAPI.DTO;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelRservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        #region HTTPGet


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Staff>))]

        public IActionResult GetBilling()
        {
            var billing = _staffRepository.GetStaff();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(billing);
        }

        [HttpGet("{LogInID}")]
        [ProducesResponseType(200, Type = typeof(Staff))]
        [ProducesResponseType(400)]

        public IActionResult GetAccesslog(int LogInID)
        {
            if (!_staffRepository.StaffExist(LogInID))
            {
                return NotFound();
            }

            var AccessLog = _staffRepository.GetStaff(LogInID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(AccessLog);
        }

        #endregion

        #region Post
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStaff([FromBody] StaffDto  staffCreate)
        {
            if (staffCreate == null)
                return BadRequest(ModelState);

            var category = _staffRepository.GetStaff()
                .Where(c => c.StaffName.Trim().ToUpper() == staffCreate.StaffName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = new Staff
            {
                StaffName = staffCreate.StaffName
            };

            if (!_staffRepository.CreateStaff(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
        #endregion

        #region Update

        [HttpPut("{StaffID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCategory(int StaffID, [FromBody] StaffDto updatedStaff)
        {
            if (updatedStaff == null)
                return BadRequest(ModelState);

            if (StaffID != updatedStaff.StaffID)
                return BadRequest(ModelState);

            if (!_staffRepository.StaffExist(StaffID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = new Staff
            {
                StaffID = updatedStaff.StaffID,
                StaffName = updatedStaff.StaffName,
                StaffRole = updatedStaff.StaffRole,
            };

            if (!_staffRepository.UpdateStaff(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{StaffID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStaff(int StaffID)
        {
            if (!_staffRepository.StaffExist(StaffID))
            {
                return NotFound();
            }

            var ReservationToDelete = _staffRepository.GetStaff(StaffID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_staffRepository.DeleteStaff(ReservationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        #endregion
    }
}

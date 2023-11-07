using HotelRservationAPI.DTO;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelRservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterController(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }


        #region HTTPGet


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Register>))]

        public IActionResult GetBilling()
        {
            var billing = _registerRepository.GetRegisters();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(billing);
        }

        [HttpGet("{UserName}")]
        [ProducesResponseType(200, Type = typeof(Register))]
        [ProducesResponseType(400)]

        public IActionResult GetAccesslog(string username)
        {
            if (!_registerRepository.RegisterExist(username))
            {
                return NotFound();
            }

            var AccessLog = _registerRepository.GetRegister(username);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(AccessLog);
        }

        #endregion

        #region Post
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRegister([FromBody] RegisterDto  registerCreate)
        {
            if (registerCreate == null)
                return BadRequest(ModelState);

            var category = _registerRepository.GetRegisters()
                .Where(c => c.Username.Trim().ToUpper() == registerCreate.Username.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = new Register
            {
                Username = registerCreate.Username,
                Password = registerCreate.Password,
                Email = registerCreate.Email
            };

            if (!_registerRepository.CreateRegister(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
        #endregion

        #region Update

        [HttpPut("{Username}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCategory(string username, [FromBody] RegisterDto updatedRegister)
        {
            if (updatedRegister == null)
                return BadRequest(ModelState);

            if (username != updatedRegister.Username)
                return BadRequest(ModelState);

            if (!_registerRepository.RegisterExist(username))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = new Register
            {
                Username = updatedRegister.Username,
                Password = updatedRegister.Password,
                Email = updatedRegister.Email,
            };

            if (!_registerRepository.UpdateRegister(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{Username}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(string username)
        {
            if (!_registerRepository.RegisterExist(username))
            {
                return NotFound();
            }

            var RegisterToDelete = _registerRepository.GetRegister(username);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_registerRepository.DeleteRegister(RegisterToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        #endregion
    }
}

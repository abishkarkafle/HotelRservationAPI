using HotelRservationAPI.DTO;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelRservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        #region HTTPGet


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]

        public IActionResult GetBilling()
        {
            var billing = _customerRepository.GetCustomer();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(billing);
        }

        [HttpGet("{CustomerID}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]

        public IActionResult GetAccesslog(int CustomerID)
        {
            if (!_customerRepository.CustomerExist(CustomerID))
            {
                return NotFound();
            }

            var AccessLog = _customerRepository.GetCustomer(CustomerID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(AccessLog);
        }

        #endregion

        #region Post

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCustomers([FromBody] CustomerDto customerCreate)
        {
            if (customerCreate == null)
                return BadRequest(ModelState);

            var category = _customerRepository.GetCustomer()
                .Where(c => c.CustomerName.Trim().ToUpper() == customerCreate.CustomerName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = new Customer
            {
                CustomerID = customerCreate.CustomerID,
                CustomerName = customerCreate.CustomerName,
                CustomerAddress = customerCreate.CustomerAddress,
                CustomerEmail = customerCreate.CustomerEmail,
                CustomerPhone = customerCreate.CustomerPhone,
                CustomerReview = customerCreate.CustomerReview,
            };

            if (!_customerRepository.CreateCustomer(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
        #endregion


        #region Update

        [HttpPut("{CustomerID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCategory(int CustomerID, [FromBody] CustomerDto updatedCustomer)
        {
            if (updatedCustomer == null)
                return BadRequest(ModelState);

            if (CustomerID != updatedCustomer.CustomerID)
                return BadRequest(ModelState);

            if (!_customerRepository.CustomerExist(CustomerID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = new Customer
            {
                CustomerID = updatedCustomer.CustomerID,
                CustomerName = updatedCustomer.CustomerName,
                CustomerAddress = updatedCustomer.CustomerAddress,
                CustomerEmail = updatedCustomer.CustomerEmail,
                CustomerPhone = updatedCustomer.CustomerPhone,
                CustomerReview = updatedCustomer.CustomerReview,
            };

            if (!_customerRepository.UpdateCustomer(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{CustomerID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int CustomerID)
        {
            if (!_customerRepository.CustomerExist(CustomerID))
            {
                return NotFound();
            }

            var CustomerToDelete = _customerRepository.GetCustomer(CustomerID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerRepository.DeleteCustomer(CustomerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

        #endregion
    }
}

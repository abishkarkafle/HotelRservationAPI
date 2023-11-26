using Azure.Core;
using HotelRservationAPI.DTO;
using HotelRservationAPI.Interface;
using HotelRservationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace HotelRservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        public static Register user = new Register();
        private readonly IConfiguration _configuration;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<ActionResult<Register>> RegisterUser(RegisterDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(RegisterDto request)
        {
            try
            {
                // Check if the username exists
                if (user.Username != request.Username)
                {
                    return BadRequest("Invalid username or password.");
                }

                // Verify the password
                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Invalid username or password.");
                }

                // If username and password are valid, create and return the JWT token
                string token = CreateToken(user);
                return Ok(token);
            }
            catch (Exception)
            {
                // Return a generic error message to the client
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred during login.");
            }
        }


        private string CreateToken(Register user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjectFor7COMm.DTOs;
using ProjectFor7COMm.Models;
using ProjectFor7COMm.Services;

namespace ProjectFor7COMm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO request)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email
            };

            await _userService.RegisterUser(user, request.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            var isValid = await _userService.ValidateUser(request.Username, request.Password);
            if (!isValid)
                return Unauthorized();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(PasswordResetDTO request)
        {
            var result = await _userService.ResetPassword(request.Email, request.NewPassword);
            if (!result)
                return BadRequest("Invalid email address.");

            return Ok("Password reset successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
                return NotFound("User not found.");

            return Ok("User deleted successfully.");
        }
    }
}

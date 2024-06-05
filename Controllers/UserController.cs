using Microsoft.AspNetCore.Mvc;
using ProjectFor7COMm.DTOs;
using ProjectFor7COMm.Models;
using ProjectFor7COMm.Services;
using System;

namespace ProjectFor7COMm.Controllers
{
    [Route("api/user")]
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
            try
            {
                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email
                };

                await _userService.RegisterUser(user, request.Password);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            try
            {
                var isValid = await _userService.ValidateUser(request.Username, request.Password);
                if (!isValid)
                    return Unauthorized();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(PasswordResetDTO request)
        {
            try
            {
                var result = await _userService.ResetPassword(request.Email, request.NewPassword);
                if (!result)
                    return BadRequest("Invalid email address.");

                return Ok("Password reset successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                if (!result)
                    return NotFound("User not found.");

                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO request)
        {
            try
            {
                var user = new User
                {
                    Id = id,
                    Username = request.Username,
                    Email = request.Email
                };

                var result = await _userService.UpdateUser(id, user);
                if (!result)
                    return BadRequest("Failed to update user.");

                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

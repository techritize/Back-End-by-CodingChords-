using Microsoft.AspNetCore.Mvc;
using TechritizeAuthAPI.Helpers;
using TechritizeAuthAPI.Models;
using TechritizeAuthAPI.Models.Requests;
using TechritizeAuthAPI.Repositories;
using TechritizeAuthAPI.Services;

namespace TechritizeAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtTokenService _jwtService;
        private readonly TwoFactorService _twoFactorService;

        public AuthController(IUserRepository userRepo, JwtTokenService jwtService, TwoFactorService twoFactorService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _twoFactorService = twoFactorService;
        }

        // 1️⃣ Signup
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                return BadRequest("Passwords do not match.");

            if (await _userRepo.EmailExistsAsync(request.Email))
                return BadRequest("Email already exists.");

            var newUser = new User
            {
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                PasswordHash = PasswordHasher.HashPassword(request.Password),
                Role = request.Role
            };

            await _userRepo.AddUserAsync(newUser);

            return Ok("User registered successfully.");
        }

        // 2️⃣ Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null) return Unauthorized("Invalid credentials.");

            if (!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var token = _jwtService.GenerateToken(user);

            return Ok(new { Token = token, Role = user.Role, Name = user.Name });
        }

        [HttpPost("recovery/request")]
        public async Task<IActionResult> RequestRecovery([FromBody] RecoveryRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            var email = request.Email?.Trim();
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email cannot be empty.");

            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null || string.IsNullOrWhiteSpace(user.Email))
                return NotFound("User not found or email is missing in the database.");

            try
            {
                await _twoFactorService.GenerateAndSendCodeAsync(user.Email);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a proper logging service here)
                return StatusCode(500, $"Failed to send recovery email: {ex.Message}");
            }

            return Ok("Recovery code sent to email.");
        }



        // 4️⃣ Recovery Step 2: Reset Password
        [HttpPost("recovery/reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null) return NotFound("User not found.");

            var isValid = await _twoFactorService.ValidateCodeAsync(request.Email, request.Code);
            if (!isValid) return BadRequest("Invalid or expired code.");

            user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);
            await _userRepo.UpdateUserAsync(user);

            return Ok("Password reset successful.");
        }
    }
}

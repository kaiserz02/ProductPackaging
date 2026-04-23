using Microsoft.AspNetCore.Mvc;
using ProductPackaging.DTOs;
using ProductPackaging.Services;

namespace ProductPackaging.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var success = await _authService.RegisterAsync(dto.Username, dto.Password);

            if (!success)
                return BadRequest("Username already exists");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var token = await _authService.LoginAsync(dto.Username, dto.Password);

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new AuthResponseDto
            {
                Token = token
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SportsManager.Api.Models;
using SportsManager.Api.Services;



namespace SportsManager.Api.Controllers
{
    [ApiController]
    [Route("api.[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.RegisterUserAsync(registrationDto);

            if (result.Success) return Ok(new { message = "User registered successfully", userId = result.UserId });

            return BadRequest(new { message = result.ErrorMessage });
        }
    }
}

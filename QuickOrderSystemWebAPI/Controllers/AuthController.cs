using Microsoft.AspNetCore.Mvc;
using QuickOrderSystemClassLibrary;
using QuickOrderSystemClassLibrary.Services.UserService;

namespace QuickOrderSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            _userService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userToRegister = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var registeredUser = await _userService.RegisterUser(userToRegister);
            if (registeredUser == null)
            {
                return BadRequest("Registration failed. Try again.");
            }
            return Ok(registeredUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserDto request)
        {
            var userFromDb = await _userService.GetUserByUsername(request.Username);

            if (userFromDb == null)
                return BadRequest("User not found.");

            if (!_userService.VerifyPasswordHash(request.Password, userFromDb.PasswordHash, userFromDb.PasswordSalt))
                return BadRequest("Wrong password.");
            
            return Ok(userFromDb.Id);
        }
    }
}
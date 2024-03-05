using Microsoft.AspNetCore.Mvc;
using RegistrationAPI.DTO;
using RegistrationAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
   

        // POST api/<UserController>
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            var createdUser = await _userRepository.RegisterAsync(userDTO);

            if (createdUser.Success == true)
                return Ok(createdUser);
            return BadRequest("Unable to create a user");

        }


        
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDTO loginDTO)
        {
            var loginUser = await _userRepository.LoginAsync(loginDTO);

            if (loginUser.Success == true)
                return Ok(loginUser);
            return BadRequest(loginUser);

        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfileAsync(GetProfile getProfile)
        {
            var getUser = await _userRepository.GetProfileAsync(getProfile);

            if (getUser.Success == true)
                return Ok(getUser);
            return BadRequest("Unable to get a user");

        }
    }

}

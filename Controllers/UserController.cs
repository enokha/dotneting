using Microsoft.AspNetCore.Mvc;
using MyBookApp.DTO;
using MyBookApp.Services;
using Microsoft.AspNetCore.Identity;

namespace MyBookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTOIn userDTOIn)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(userDTOIn);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetAllUsers), new { userDTOIn.Username });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTOIn userDTOIn)
        {
            var result = await _userService.UpdateUserAsync(id, userDTOIn);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return NoContent();
        }
    }
}

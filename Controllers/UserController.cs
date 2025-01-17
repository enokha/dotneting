using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBookApp.Data;
using MyBookApp.Models;
using MyBookApp.DTO;

namespace MyBookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly AppDbContext _context;

        public UserController(UserManager<UserModel> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            var userDTOs = users.Select(user => new UserDTOOut
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                City = user.City,
                MembershipStatus = user.MembershipStatus,
                Email = user.Email
            });

            return Ok(userDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTOIn userDTOIn)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newUser = new UserModel
            {
                UserName = userDTOIn.Username,
                Firstname = userDTOIn.Firstname,
                Lastname = userDTOIn.Lastname,
                City = userDTOIn.City,
                Email = userDTOIn.Email,
                MembershipStatus = userDTOIn.MembershipStatus
            };

            var result = await _userManager.CreateAsync(newUser, userDTOIn.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetAllUsers), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTOIn userDTOIn)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null) return NotFound($"User with ID {id} not found.");

            existingUser.Firstname = userDTOIn.Firstname;
            existingUser.Lastname = userDTOIn.Lastname;
            existingUser.City = userDTOIn.City;
            existingUser.MembershipStatus = userDTOIn.MembershipStatus;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound($"User with ID {id} not found.");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBookApp.DTO;
using MyBookApp.Models;

namespace MyBookApp.Services
{
    public class UserService
    {
        private readonly UserManager<UserModel> _userManager;

        public UserService(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserDTOOut>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(user => new UserDTOOut
            {
                Id = user.Id,
                Firstname = user.Firstname ?? string.Empty,
                Lastname = user.Lastname ?? string.Empty,
                City = user.City ?? string.Empty,
                MembershipStatus = (int)user.MembershipStatus, // ✅ Explicit enum conversion
                Email = user.Email ?? string.Empty
            }).ToList();
        }

        public async Task<UserDTOOut?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return new UserDTOOut
            {
                Id = user.Id,
                Firstname = user.Firstname ?? string.Empty,
                Lastname = user.Lastname ?? string.Empty,
                City = user.City ?? string.Empty,
                MembershipStatus = (int)user.MembershipStatus, // ✅ Explicit conversion
                Email = user.Email ?? string.Empty
            };
        }

        public async Task<IdentityResult> CreateUserAsync(UserDTOIn userDTOIn)
        {
            var newUser = new UserModel
            {
                UserName = userDTOIn.Username,
                Firstname = userDTOIn.Firstname ?? string.Empty,
                Lastname = userDTOIn.Lastname ?? string.Empty,
                City = userDTOIn.City ?? string.Empty,
                Email = userDTOIn.Email ?? string.Empty,
                MembershipStatus = (MemberStatus)userDTOIn.MembershipStatus 
            };

            return await _userManager.CreateAsync(newUser, userDTOIn.Password ?? string.Empty);
        }

        public async Task<IdentityResult> UpdateUserAsync(string id, UserDTOIn userDTOIn)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null) return IdentityResult.Failed();

            existingUser.Firstname = userDTOIn.Firstname ?? string.Empty;
            existingUser.Lastname = userDTOIn.Lastname ?? string.Empty;
            existingUser.City = userDTOIn.City ?? string.Empty;
            existingUser.MembershipStatus = (MemberStatus)userDTOIn.MembershipStatus; 
            return await _userManager.UpdateAsync(existingUser);
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return IdentityResult.Failed();

            return await _userManager.DeleteAsync(user);
        }
    }
}

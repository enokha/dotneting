using MyBookApp.Models;

namespace MyBookApp.DTO
{
    public class UserDTOIn
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public MemberStatus MembershipStatus { get; set; }
    }
}

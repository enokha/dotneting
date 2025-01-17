using MyBookApp.Models;

namespace MyBookApp.DTO
{
    public class UserDTOOut
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public MemberStatus MembershipStatus { get; set; }
        public string Email { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace MyBookApp.Models
{
    public enum MemberStatus
    {
        Active,
        Pending,
        Inactive,
        Suspended
    }

    public class UserModel : IdentityUser
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public MemberStatus MembershipStatus { get; set; } = MemberStatus.Inactive;
    }
}

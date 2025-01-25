namespace MyBookApp.DTO
{
    public class UserDTOOut
    {
        public string Id { get; set; } = string.Empty; 
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int MembershipStatus { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}

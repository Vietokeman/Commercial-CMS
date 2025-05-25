namespace CMS.Core.Models.System
{
    public class UpdateUserRequest
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }

    }
}

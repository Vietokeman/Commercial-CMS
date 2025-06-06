﻿namespace CMS.Core.Models.System
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public string? Avatar { get; set; }
    }
}

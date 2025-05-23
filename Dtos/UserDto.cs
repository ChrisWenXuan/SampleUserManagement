﻿namespace SampleUserManagement.Dtos
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
}

﻿namespace SharedSilicon.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Biography { get; set; }
    }
}

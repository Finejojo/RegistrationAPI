﻿using System.ComponentModel.DataAnnotations;

namespace RegistrationAPI.DTO
{
    public class UserDTO
    {
       
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

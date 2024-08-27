﻿using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "username is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
    }
}

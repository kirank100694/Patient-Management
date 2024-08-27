﻿using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "username is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string? Password { get; set; }
    }
}

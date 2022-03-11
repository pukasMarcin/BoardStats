﻿using System.ComponentModel.DataAnnotations;

namespace BoardStats.Data.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember me?")]
        public bool Rem { get; set; }





    }
}

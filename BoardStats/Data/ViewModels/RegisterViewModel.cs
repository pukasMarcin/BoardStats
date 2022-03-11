using System.ComponentModel.DataAnnotations;

namespace BoardStats.Data.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]   
        [StringLength(100, ErrorMessage ="Hasło musi zawierać conajmniej {1} znaków",MinimumLength =6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Wprowadzone hasła nie są jednakowe!")]
        public string ConfirmPassword { get; set; }

        
    }
}

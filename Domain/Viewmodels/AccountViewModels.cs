using System.ComponentModel.DataAnnotations;

namespace BEP.BL.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "De ingegeven postcode moet exact 4 cijfers lang zijn.")]
        [MaxLength(4, ErrorMessage = "De ingegeven postcode moet exact 4 cijfers lang zijn.")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Postcode mag enkel cijfers bevatten.")]
        [Display(Name = "Postcode")]
        public string Zip { get; set; }
        [Required]
        [Display(Name = "Voornaam")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Achternaam")]
        public string Lastname { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het {0} moet op zijn minst {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en de bevestiging van het wachtwoord moeten gelijk zijn.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "De ingegeven postcode moet exact 4 cijfers lang zijn.")]
        [MaxLength(4, ErrorMessage = "De ingegeven postcode moet exact 4 cijfers lang zijn.")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Postcode mag enkel cijfers bevatten.")]
        [Display(Name = "Postcode")]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "Voornaam")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string Lastname { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het {0} moet op zijn minst {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en de bevestiging van het wachtwoord moeten gelijk zijn.")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

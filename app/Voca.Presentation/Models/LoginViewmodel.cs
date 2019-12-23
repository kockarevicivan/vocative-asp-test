using System.ComponentModel.DataAnnotations;

namespace Voca.Presentation.Models
{
    public class LoginViewmodel
    {
        [Required(ErrorMessage = "E-mail is required.")]
        [EmailAddress(ErrorMessage = "You must enter a valid e-mail address.")]
        [StringLength(256, ErrorMessage = "You must enter a valid e-mail address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        // Maximum length for password is 20 characters.
        [StringLength(20, ErrorMessage = "Password incorrect.")]
        public string Password { get; set; }
        public string RememberMe { get; set; }
    }
}
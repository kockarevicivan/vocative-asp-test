using System.ComponentModel.DataAnnotations;

namespace Voca.Presentation.Models
{
    public class RegisterViewmodel
    {
        [Required(ErrorMessage = "E-mail is required.")]
        [EmailAddress(ErrorMessage = "You must enter a valid e-mail address.")]
        [StringLength(256, ErrorMessage = "Maximum length for e-mail is 256 characters.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Maximum length for password is 20 characters.")]
        public string Password { get; set; }
    }
}
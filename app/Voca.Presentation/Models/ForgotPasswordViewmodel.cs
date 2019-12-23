using System.ComponentModel.DataAnnotations;

namespace Voca.Presentation.Models
{
    public class ForgotPasswordViewmodel
    {
        [Required(ErrorMessage = "E-mail is required.")]
        [EmailAddress(ErrorMessage = "You must enter a valid e-mail address.")]
        [StringLength(256, ErrorMessage = "You must enter a valid e-mail address.")]
        public string Email { get; set; }
    }
}
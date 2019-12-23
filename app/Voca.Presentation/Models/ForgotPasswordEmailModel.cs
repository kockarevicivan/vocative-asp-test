using System.ComponentModel.DataAnnotations;

namespace Voca.Presentation.Models
{
    public class ForgotPasswordEmailModel
    {
        public string Token { get; set; }
    }
}
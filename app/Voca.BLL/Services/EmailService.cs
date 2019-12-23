using System.Net;
using System.Net.Mail;

namespace Voca.BLL.Services
{
    public class EmailService
    {
        private MailAddress _fromAddress;
        private MailAddress _adminAddress;
        private SmtpClient _smtpClient;

        public EmailService()
        {
            _fromAddress = new MailAddress("");
            _adminAddress = new MailAddress("");

            _smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_fromAddress.Address, "")
            };
        }

        public void SendNewNounEmail(string newNoun)
        {
            string body = "<p>System has detected a new noun: <b>" + newNoun + "</b></p>";

            using (var message = new MailMessage(_fromAddress, _adminAddress)
            {
                Subject = "New noun submitted",
                Body = body,
                IsBodyHtml = true
            })
            {
                _smtpClient.Send(message);
            }
        }

        public void SendVerificationEmail(string email, string verificationToken)
        {
            string body = "<p>Welcome to Voca API! Click the button to complete the registration:</p><form method=\"POST\" action=\"" + "http://localhost:49258" + "/Account/ForgotPasswordEmailLink\"><input name=\"Token\" type=\"hidden\" value=\"" + verificationToken + "\" /><input type=\"submit\" value=\"Verify e-mail\" /></form>";
            var toAddress = new MailAddress(email);

            using (var message = new MailMessage(_fromAddress, toAddress)
            {
                Subject = "Welcome to Voca API",
                Body = body,
                IsBodyHtml = true
            })
            {
                _smtpClient.Send(message);
            }
        }

        public void SendResetPasswordEmail(string email, string newPassword)
        {
            string body = "<p>Your password on Voca API is now: <b>" + newPassword + "</b></p>";
            var toAddress = new MailAddress(email);

            using (var message = new MailMessage(_fromAddress, toAddress)
            {
                Subject = "Your password has been reset",
                Body = body,
                IsBodyHtml = true
            })
            {
                _smtpClient.Send(message);
            }
        }
    }
}

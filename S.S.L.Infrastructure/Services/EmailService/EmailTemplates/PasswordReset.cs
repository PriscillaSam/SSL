using S.S.L.Domain.Common;
using S.S.L.Domain.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Services.EmailService.EmailTemplates
{
    public class PasswordReset : GenericEmailTemplate
    {
        private UserModel _user;

        public PasswordReset(UserModel user)
        {
            _user = user;

        }

        public override async Task GenerateEmailAsync()
        {
            var emailService = SmtpService.Instance;
            //Url.Action("About", "Home", null, Request.Url.Scheme)

            var body = this[nameof(PasswordReset)]
                .Replace("{user}", _user.FullName)
                .Replace("{url}", ConfigSettings.Instance.Domain.PasswordResetUrl.Replace("{userId}", _user.Id.ToString()));

            var mailMessage = new MailMessage
            {
                Subject = "Password Reset For SSL",
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(_user.Email));
            await emailService.SendAsync(mailMessage);

        }
    }
}

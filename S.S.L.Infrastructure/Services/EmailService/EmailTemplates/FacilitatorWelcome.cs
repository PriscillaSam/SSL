using S.S.L.Domain.Common;
using S.S.L.Domain.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Services.EmailService.EmailTemplates
{
    public class FacilitatorWelcome : GenericEmailTemplate
    {
        private readonly UserModel _user;

        public FacilitatorWelcome(UserModel user)
        {
            _user = user;
        }
        public override async Task GenerateEmailAsync()
        {
            var emailService = SmtpService.Instance;
            var body = this[nameof(FacilitatorWelcome)]
                .Replace("{user}", _user.FirstName)
                .Replace("{url}", ConfigSettings.Instance.Domain.PasswordResetUrl);

            var mailMessage = new MailMessage
            {
                Subject = "Welcome Facilitator",
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(_user.Email));
            await emailService.SendAsync(mailMessage);
        }
    }
}

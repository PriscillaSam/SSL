using S.S.L.Domain.Common;
using S.S.L.Domain.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Services.EmailService.EmailTemplates
{

    public class AccountConfirmation : GenericEmailTemplate
    {
        private UserModel _user;
        private readonly ConfigSettings _settings;

        public AccountConfirmation(UserModel user)
        {
            _user = user;
            _settings = ConfigSettings.Instance;
        }

        public override async Task GenerateEmailAsync()
        {
            var emailService = SmtpService.Instance;

            var body = this[nameof(AccountConfirmation)].Replace("{url}", _settings.Domain.EmailConfirmUrl.Replace("{userId}", $"{_user.Id}")).Replace("{user}", _user.FullName);

            var mailMessage = new MailMessage
            {
                Subject = "Account Confirmation",
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(_user.Email));
            await emailService.SendAsync(mailMessage);

        }


    }
}

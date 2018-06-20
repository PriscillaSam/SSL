using S.S.L.Domain.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Services.EmailService.EmailTemplates
{
    public class MenteeWelcome : GenericEmailTemplate
    {
        private readonly UserModel _mentee;

        public MenteeWelcome(UserModel mentee)
        {
            _mentee = mentee;

        }

        public override async Task GenerateEmailAsync()
        {
            var emailService = SmtpService.Instance;

            var body = this[nameof(MenteeWelcome)].Replace("{user}", _mentee.FullName);

            var mailMessage = new MailMessage
            {
                Subject = "Welcome To Sacred Secular League",
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(_mentee.Email));
            await emailService.SendAsync(mailMessage);
        }
    }


}

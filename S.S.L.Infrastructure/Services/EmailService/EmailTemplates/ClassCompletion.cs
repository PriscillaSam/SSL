using S.S.L.Domain.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Services.EmailService.EmailTemplates
{
    public class ClassCompletion : GenericEmailTemplate
    {
        private readonly UserModel _user;

        public ClassCompletion(UserModel user)
        {
            _user = user;
        }

        public override async Task GenerateEmailAsync()
        {
            var emailService = SmtpService.Instance;

            var body = this[nameof(ClassCompletion)].Replace("{user}", _user.FirstName);

            var mailMessage = new MailMessage
            {
                Subject = "Congratulations on completing your classes",
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(_user.Email));
            await emailService.SendAsync(mailMessage);
        }
    }
}

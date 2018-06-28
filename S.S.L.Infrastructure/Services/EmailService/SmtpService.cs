using S.S.L.Domain.Common;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Services.EmailService
{
    public class SmtpService
    {
        private static readonly Lazy<SmtpService> Service =
          new Lazy<SmtpService>(() => new SmtpService(), true);

        public static SmtpService Instance => Service.Value;
        private readonly ConfigSettings _settings;

        public SmtpService()
        {
            _settings = ConfigSettings.Instance;
        }
        public async Task SendAsync(MailMessage message)
        {
            var smtp = _settings.Smtp;
            var user = _settings.User;

            var smtpClient = new SmtpClient
            {
                Host = smtp.Host,
                Port = smtp.Port,
                EnableSsl = smtp.EnableSsl,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(user.Username, user.Password)
            };

            message.From = new MailAddress(user.Username, smtp.Sender);

            await smtpClient.SendMailAsync(message);

        }

    }
}

using System.Net.Mail;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Services.EmailService.EmailTemplates
{
    public class MenteeAssignment : GenericEmailTemplate
    {
        private readonly MenteeFacilitatorModel _model;

        public MenteeAssignment(MenteeFacilitatorModel model)
        {
            _model = model;
        }

        public async override Task GenerateEmailAsync()
        {
            var emailService = SmtpService.Instance;

            var menteeBody = this[nameof(MenteeAssignment)]
                .Replace("{user}", _model.MenteeFirstName)
                .Replace("{assigned}", _model.FacilitatorFirstName)
                .Replace("{fullName}", _model.FacilitatorName)
                .Replace("{email}", _model.FacilitatorEmail)
                .Replace("{message}", "You have been assigned a Facilitator");


            var facilitatorBody = this[nameof(MenteeAssignment)]
                .Replace("{user}", _model.FacilitatorFirstName)
                .Replace("{assigned}", _model.MenteeFirstName)
                .Replace("{fullName}", _model.MenteeName)
                .Replace("{email}", _model.MenteeEmail)
                .Replace("{message}", "You have a new mentee asignment")
                .Replace("<span></span>", $" or phone number <strong>{_model.MenteeNumber}</strong>.");

            var menteeEmail = new MailMessage
            {
                Subject = "Facilitator Assignment",
                Body = menteeBody,
                IsBodyHtml = true
            };

            var facilitatorEmail = new MailMessage
            {
                Subject = "New Mentee Assignment",
                Body = facilitatorBody,
                IsBodyHtml = true
            };

            menteeEmail.To.Add(new MailAddress(_model.MenteeEmail));
            facilitatorEmail.To.Add(new MailAddress(_model.FacilitatorEmail));

            await emailService.SendAsync(menteeEmail);
            await emailService.SendAsync(facilitatorEmail);

        }
    }
}

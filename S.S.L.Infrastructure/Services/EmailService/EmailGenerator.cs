using S.S.L.Domain.Enums;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.Services.EmailService.EmailTemplates;
namespace S.S.L.Infrastructure.Services.EmailService
{
    public class EmailGenerator
    {

        public GenericEmailTemplate GetTemplate(EmailType type, object param)
        {
            switch (type)
            {
                case EmailType.AccountConfirmation:
                    return new AccountConfirmation(param as UserModel);
                case EmailType.MenteeWelcome:
                    return new MenteeWelcome(param as UserModel);
                case EmailType.PasswordReset:
                    return new PasswordReset(param as UserModel);
                case EmailType.MenteeAssignment:
                    return new MenteeAssignment(param as MenteeFacilitatorModel);
                case EmailType.MenteeNextSteps:
                    return new MenteeNextSteps(param as UserModel);
                default:
                    return null;

            }
        }
    }
}

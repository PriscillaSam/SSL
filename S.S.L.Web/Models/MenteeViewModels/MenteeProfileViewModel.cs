using S.S.L.Domain.Models;

namespace S.S.L.Web.Models.MenteeViewModels
{
    public class MenteeProfileViewModel
    {
        public UserModel Mentee { get; set; }
        public UserModel Facilitator { get; set; }

    }
}
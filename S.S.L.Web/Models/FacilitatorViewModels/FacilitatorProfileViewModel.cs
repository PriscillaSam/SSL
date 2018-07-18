using S.S.L.Domain.Models;
using System.Collections.Generic;

namespace S.S.L.Web.Models.FacilitatorViewModels
{
    public class FacilitatorProfileViewModel
    {

        public UserModel Facilitator { get; set; }
        public IEnumerable<MenteeUserModel> Mentees { get; set; }
    }
}
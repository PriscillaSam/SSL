using S.S.L.Domain.Enums;
using S.S.L.Domain.Models;
using System.Collections.Generic;

namespace S.S.L.Web.Models.AdminViewModels
{
    public class MenteesViewModel
    {

        public IEnumerable<MenteeUserModel> Mentees { get; set; }
        public GymGroup GymGroup { get; set; }

        public int MenteeId { get; set; }
    }
}
using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace S.S.L.Web.Models.AdminViewModels
{
    public class UnassignedMenteeViewModel
    {
        public IEnumerable<UserModel> Mentees { get; set; }
        public SelectList Facilitators { get; set; }

    }
}
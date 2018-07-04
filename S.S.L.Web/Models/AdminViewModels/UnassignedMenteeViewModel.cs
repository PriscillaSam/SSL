using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S.S.L.Web.Models.AdminViewModels
{
    public class UnassignedMenteeViewModel
    {
        public IEnumerable<UserModel> Mentees { get; set; }

        [Required(ErrorMessage = "please select a gender")]
        public string Gender { get; set; }
        [Required]
        public int FacilitatorId { get; set; }
        public int MenteeId { get; set; }

    }
}
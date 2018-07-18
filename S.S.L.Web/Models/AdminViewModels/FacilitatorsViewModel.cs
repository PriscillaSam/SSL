using S.S.L.Domain.Enums;
using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S.S.L.Web.Models.AdminViewModels
{
    public class FacilitatorsViewModel
    {
        public IEnumerable<UserModel> Facilitators { get; set; }


        //Fields for facilitator signup
        [Required(ErrorMessage = "please enter first name"), Display(Name = "First Name"), MinLength(2, ErrorMessage = "first name can't be less than two characters ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "please enter last name"), Display(Name = "Last Name"), MinLength(2, ErrorMessage = "last name can't be less than two characters ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "please enter valid email address"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "please the gender field is required")]
        public string Gender { get; set; }

        [Display(Name = "Make Admin")]
        public bool MakeAdmin { get; set; }


        //fields for gym assignment
        public int UserId { get; set; }
        public GymGroup GymGroup { get; set; }
    }
}
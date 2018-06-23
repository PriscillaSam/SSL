using S.S.L.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace S.S.L.Web.Models.AdminViewModels
{
    public class FacilitatorSignUpViewModel
    {
        [Required(ErrorMessage = "please enter first name"), Display(Name = "First Name"), MinLength(2, ErrorMessage = "first name can't be less than two characters ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "please enter last name"), Display(Name = "Last Name"), MinLength(2, ErrorMessage = "last name can't be less than two characters ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "please enter valid email address"), EmailAddress]
        [EmailValidator]
        public string Email { get; set; }

        [Display(Name = "Make Admin")]
        public bool MakeAdmin { get; set; }


    }
}
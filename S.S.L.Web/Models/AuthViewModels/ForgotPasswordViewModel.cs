using System.ComponentModel.DataAnnotations;

namespace S.S.L.Web.Models.AuthViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "please enter your email"), EmailAddress(ErrorMessage = "please enter a valid email")]
        public string Email { get; set; }

    }
}
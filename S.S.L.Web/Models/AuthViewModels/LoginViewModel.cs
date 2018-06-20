using System.ComponentModel.DataAnnotations;

namespace S.S.L.Web.Models.AuthViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "please enter your email"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter your password"), DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
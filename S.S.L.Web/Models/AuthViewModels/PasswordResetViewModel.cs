using System.ComponentModel.DataAnnotations;

namespace S.S.L.Web.Models.AuthViewModels
{
    public class PasswordResetViewModel
    {
        [Required(ErrorMessage = "please enter your email"), EmailAddress(ErrorMessage = "please enter a valid email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "please enter your password"),
        DataType(DataType.Password),
        Display(Name = "New Password"),
        MinLength(8, ErrorMessage = "New password length should be at least 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "please confirm your password"),
        DataType(DataType.Password),
        Display(Name = "Confirm Password"),
        Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
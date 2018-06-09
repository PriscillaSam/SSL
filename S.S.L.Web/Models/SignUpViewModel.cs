using S.S.L.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S.S.L.Web.Models
{
    public class SignUpViewModel : BaseModel
    {

        [Required(ErrorMessage = "please enter your first name"), Display(Name = "First Name"), MinLength(2, ErrorMessage = "first name should not be less than two characters")]
        public string FirstName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage = "please enter your last name"), Display(Name = "Last Name"), MinLength(2, ErrorMessage = "last name should not be less than two characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "please enter a valid email address"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter a valid phone number"), Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "select your state")]
        public string State { get; set; }

        [Required(ErrorMessage = "select your country")]
        public string Country { get; set; }

        [Required, Display(Name = "Employment status")]
        public string EmploymentStatus { get; set; }

        [Required(ErrorMessage = "please enter your password"), DataType(DataType.Password), MinLength(8, ErrorMessage = "Password length should be at least 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "please confirm your password"), DataType(DataType.Password), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult("Passwords do not match", new[] { nameof(Password), nameof(ConfirmPassword) });

            }
        }
    }


}
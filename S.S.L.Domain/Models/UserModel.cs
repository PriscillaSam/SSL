using S.S.L.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S.S.L.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter first name"), MinLength(2, ErrorMessage = "first name should not be less than two characters")]
        public string FirstName { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "please enter first name"), MinLength(2, ErrorMessage = "last name should not be less than two characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "please enter phone number"), RegularExpression(pattern: @"^[0-9]+$", ErrorMessage = "please enter a valid phone number"), MinLength(length: 11, ErrorMessage = "mobile number should be at least 11 digits")]
        public string MobileNumber { get; set; }
        public string EmploymentStatus { get; set; }

        [Required(ErrorMessage = "select your country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "select your state")]

        public string State { get; set; }

        public UserType UserType { get; set; }
        public GymGroup GymGroup { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public IEnumerable<string> Roles { get; set; }
    }
}

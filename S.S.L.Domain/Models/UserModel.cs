using S.S.L.Domain.Enums;
using System.Collections.Generic;

namespace S.S.L.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmploymentStatus { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public UserType UserType { get; set; }
        public GymGroup GymGroup { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public IEnumerable<string> Roles { get; set; }
    }
}

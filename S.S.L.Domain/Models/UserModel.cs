using S.S.L.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string FullName => $"{FirstName} {LastName}";

    }
}


using S.S.L.Domain.Enums;
using System.Collections.Generic;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class User : BaseModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string MobileNumber { get; set; }
        public string ImageUrl { get; set; }
        public string PasswordHash { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }
        public string EmploymentStatus { get; set; }
        public bool EmailConfirmed { get; set; }
        public GymGroup GymGroup { get; set; }
        public bool IsDeleted { get; set; }


        //location
        public string Country { get; set; }
        public string State { get; set; }


        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Todo> Todos { get; set; }
        public ICollection<Schedule> Schedules { get; set; }


        public User()
        {
            UserRoles = new HashSet<UserRole>();
            Todos = new HashSet<Todo>();
            Schedules = new HashSet<Schedule>();
        }



    }
}

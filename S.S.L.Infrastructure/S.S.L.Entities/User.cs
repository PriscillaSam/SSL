
using System.Collections.Generic;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class User : BaseModel
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string ImageUrl { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }
        //Relationships
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class UserRole
    {
        [Key]
        [Column(Order = 1)]
        public int RoleId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int UserId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }

        public UserRole()
        {
        }

        public UserRole(Role role, User user)
        {
            Role = role;
            User = user;
        }

    }
}

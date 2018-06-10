using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class UserRole : BaseModel
    {
        
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }

    }
}

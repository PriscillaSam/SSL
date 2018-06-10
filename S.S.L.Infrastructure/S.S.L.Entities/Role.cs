using System.Collections.Generic;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Role : BaseModel
    {
        public Role() => UserRoles = new HashSet<UserRole>();

        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; private set; }
    }
}
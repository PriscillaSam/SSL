using System.Collections.Generic;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Role : BaseModel
    {
        public Role() => Users = new HashSet<User>();

        public string Name { get; set; }
        public ICollection<User> Users { get; private set; }
    }
}
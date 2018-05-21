using System.Collections.Generic;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Role : Model
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
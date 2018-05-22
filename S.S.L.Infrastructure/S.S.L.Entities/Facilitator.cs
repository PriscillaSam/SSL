using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Facilitator : BaseModel
    {
        public Facilitator() => Mentees = new HashSet<Mentee>();


        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Mentee> Mentees { get; private set; }
    }
}

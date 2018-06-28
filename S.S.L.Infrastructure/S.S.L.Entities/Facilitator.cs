using System.Collections.Generic;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Facilitator : BaseModel
    {
        public Facilitator() => Mentees = new HashSet<Mentee>();


        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Mentee> Mentees { get; set; }
    }
}

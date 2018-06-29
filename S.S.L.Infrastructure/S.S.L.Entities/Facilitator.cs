using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Facilitator : BaseModel
    {
        public Facilitator() => Mentees = new HashSet<Mentee>();

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Mentee> Mentees { get; set; }
    }
}

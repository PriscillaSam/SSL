using System.Collections.Generic;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Mentee : BaseModel
    {
        public Mentee() => Schedules = new HashSet<Schedule>();
        

        public int UserId { get; set; }
        public User User { get; set; }


        public int FacilitatorId { get; set; }
        public Facilitator Facilitator { get; set; }
        public ICollection<Schedule> Schedules { get; private set; }
    }
}
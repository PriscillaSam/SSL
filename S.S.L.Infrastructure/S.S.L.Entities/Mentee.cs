using System.ComponentModel.DataAnnotations.Schema;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Mentee : BaseModel
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Facilitator")]
        public int FacilitatorId { get; set; }
        public virtual Facilitator Facilitator { get; set; }
    }
}
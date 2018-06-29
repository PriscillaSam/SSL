using S.S.L.Domain.Enums;
using System;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Schedule : BaseModel
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Detail { get; set; }
        public ScheduleStatus Status { get; set; }

        //Relationship. Mentee userId reference
        public int UserId { get; set; }
        public User User { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Schedule : BaseModel
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Detail { get; set; }

        //Relationship. Mentee userId reference
        public int MenteeId { get; set; }
        public Mentee Mentee { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Comment : BaseModel
    {
        public string Message { get; set; }
     
        //Relationships
        public int UserId { get; set; }
        public User User { get; set; }

        public int ForumId { get; set; }
        public Forum Forum { get; set; }

    }
}

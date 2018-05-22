using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Forum : BaseModel
    {
        public Forum() => Comments = new HashSet<Comment>();

        public string Title { get; set; }
        public string Detail { get; set; }
        public string Scriptures { get; set; }


        public ICollection<Comment> Comments { get; private set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Country: BaseModel
    {
        public Country() => States = new HashSet<State>();
        
        public string Name { get; set; }

        public ICollection<State> States { get; set; }

    }
}

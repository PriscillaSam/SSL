using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class State : BaseModel
    {
        public string Name { get; set; }


        public int CountryId { get; set; }
        public Country Country { get; set; }

    }
}

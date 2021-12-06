using System;
using System.Collections.Generic;
using System.Text;

namespace HW_7
{
    class Player
    {
        public string Name { get; set; }
        public string PlayerId { get; set; }
        public string CountryId { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}

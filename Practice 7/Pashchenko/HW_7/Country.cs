using System;
using System.Collections.Generic;
using System.Text;

namespace HW_7
{
    class Country
    {
        public string Name { get; set; }
        public string CountryId { get; set; }
        public int AreaSqkm { get; set; }
        public int Population { get; set; }
        
        public virtual ICollection<Olympiad> Olympiads { get; set; }
    }
}

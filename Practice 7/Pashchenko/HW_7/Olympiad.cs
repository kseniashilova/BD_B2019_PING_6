using System;
using System.Collections.Generic;
using System.Text;

namespace HW_7
{
    class Olympiad
    {
        public string OlympiadId { get; set; }
        public string CountryId { get; set; }
        public string City { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public virtual Country Country { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}

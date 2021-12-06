using System;
using System.Collections.Generic;

namespace Hse.DbGenerator.Models
{
    public class Olympic
    {
        public const int OlympicIdMaxLength = 7;
        public string OlympicId { get; set; }
        
        public const int CountryIdMaxLength = 3;
        public string CountryId { get; set; }

        public const int CityMaxLength = 50;
        public string City { get; set; }
        
        public int Year { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public Country Country { get; set; }
        
        public ICollection<Event> Events { get; set; }
    }
}
using System.Collections.Generic;

namespace Hse.DbGenerator.Models
{
    public class Country
    {
        public const int NameMaxLength = 40;
        public string Name { get; set; }
        
        public const int CountryIdMaxLength = 3;
        public string CountryId { get; set; }
        
        public int AreaSqkm { get; set; }
        
        public int Population { get; set; }
        
        public ICollection<Olympic> Olympics { get; set; }
        
        public ICollection<Player> Players { get; set; }
    }
}
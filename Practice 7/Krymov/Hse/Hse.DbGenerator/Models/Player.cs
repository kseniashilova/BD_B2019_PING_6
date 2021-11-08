using System;

namespace Hse.DbGenerator.Models
{
    public class Player
    {
        public const int NameMaxLength = 40;
        public string Name { get; set; }
        
        public const int PlayerIdMaxLength = 10;
        public string PlayerId { get; set; }
        
        public const int CountryIdMaxLength = 3;
        public string CountryId { get; set; }
        
        public DateTime Birthdate { get; set; }
        
        public Country Country { get; set; }
    }
}
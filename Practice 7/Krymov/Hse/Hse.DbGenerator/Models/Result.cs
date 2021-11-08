namespace Hse.DbGenerator.Models
{
    public class Result
    {
        public const int EventIdMaxLength = 7;
        public string EventId { get; set; }
        
        public const int PlayerIdMaxLength = 10;
        public string PlayerId { get; set; }

        public const int MedalMaxLength = 7;
        public string Medal { get; set; }
        
        public double Value { get; set; }
        
        public Player Player { get; set; }
        
        public Event Event { get; set; }
    }
}
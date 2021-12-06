namespace Hse.DbGenerator.Models
{
    public class Event
    {
        public const int EventIdMaxLength = 7;
        public string EventId { get; set; }

        public const int NameMaxLength = 40;
        public string Name { get; set; }
        
        public string EventType { get; set; }

        public const int OlympicIdMaxLength = 7;
        public string OlympicId { get; set; }
        
        public int IsTeamEvent { get; set; }
        
        public int NumPlayersInTeam { get; set; }

        public const int ResultNotedInMaxLength = 100;
        public string ResultNotedIn { get; set; }
        
        public Olympic Olympic { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace HW_7
{
    class Event
    {
        public string EventId { get; set; }
        public string Name { get; set; }
        public string EventType { get; set; }
        public string OlympiadId { get; set; }
        public int IsTeamEvent { get; set; }
        public int NumPlayersInTeam { get; set; }
        public string ResultNotedIn { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual Olympiad Olympiad { get; set; }
    }
}

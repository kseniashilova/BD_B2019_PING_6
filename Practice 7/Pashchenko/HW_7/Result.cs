using System;
using System.Collections.Generic;
using System.Text;

namespace HW_7
{
    class Result
    {
        public string EventId { get; set; }
        public string PlayerId { get; set; }
        public string Medal { get; set; }
        public double PlayerResult { get; set; }

        public virtual Player Player { get; set; }
        public virtual Event Event { get; set; }
        public string ResultId { get; set; }
    }
}

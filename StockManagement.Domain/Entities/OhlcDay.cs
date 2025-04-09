using System;

namespace StockManagement.Domain.Entities
{
    public class OhlcDay : OhlcBase
    {
        public long? IdMonth { get; set; }
        public long? IdWeek { get; set; }

        // Navigation properties
        public Asset Asset { get; set; }
    }
} 
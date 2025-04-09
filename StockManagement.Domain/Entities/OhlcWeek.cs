using System;

namespace StockManagement.Domain.Entities
{
    public class OhlcWeek : OhlcBase
    {
        public long? IdMonth { get; set; }

        // Navigation properties
        public Asset Asset { get; set; }
    }
} 
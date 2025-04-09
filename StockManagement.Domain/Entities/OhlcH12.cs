using System;

namespace StockManagement.Domain.Entities
{
    public class OhlcH12 : OhlcBase
    {
        public long? IdMonth { get; set; }
        public long? IdWeek { get; set; }
        public long? IdDay { get; set; }

        // Navigation properties
        public Asset Asset { get; set; }
    }
} 
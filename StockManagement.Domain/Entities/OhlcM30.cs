using System;

namespace StockManagement.Domain.Entities
{
    public class OhlcM30 : OhlcBase
    {
        public long? IdMonth { get; set; }
        public long? IdWeek { get; set; }
        public long? IdDay { get; set; }
        public long? IdH12 { get; set; }
        public long? IdH4 { get; set; }
        public long? IdH1 { get; set; }

        // Navigation properties
        public Asset Asset { get; set; }
    }
} 
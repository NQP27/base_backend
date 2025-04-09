using System;

namespace StockManagement.Domain.Entities
{
    public class OhlcMonth : OhlcBase
    {
        // Navigation properties
        public Asset Asset { get; set; }
    }
} 
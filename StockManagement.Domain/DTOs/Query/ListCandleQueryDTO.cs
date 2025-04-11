namespace StockManagement.Domain.DTOs.Query
{
    public class ListCandleQueryDTO
    {
        public long? id_month { get; set; }
        public long? id_week { get; set; }
        public long? id_day { get; set; }
        public long? id_h12 { get; set; }
        public long? id_h4 { get; set; }
        public long? id_h1 { get; set; }
        public long? id_m30 { get; set; }
        public long? id_m15 { get; set; }
        public long? id_m5 { get; set; }
        public int TimeFrame { get; set; }
        public string Symbol { get; set; }
        public string Broker { get; set; }
        public DateTime? From { get; set; } = null;
        public DateTime? To { get; set; } = null;
    }
}

namespace StockManagement.Domain.Entities
{
    public class ohlc_m5 : OhlcBase
    {
        public long? id_month { get; set; }
        public long? id_week { get; set; }
        public long? id_day { get; set; }
        public long? id_h12 { get; set; }
        public long? id_h4 { get; set; }
        public long? id_h1 { get; set; }
        public long? id_m30 { get; set; }
        public long? id_m15 { get; set; }
    }
}
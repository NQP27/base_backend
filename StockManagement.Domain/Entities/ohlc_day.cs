namespace StockManagement.Domain.Entities
{
    public class ohlc_day : OhlcBase
    {
        public long? id_month { get; set; }
        public long? id_week { get; set; }
    }
}
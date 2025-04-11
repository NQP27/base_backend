namespace StockManagement.Domain.Entities
{
    public class ohlc_h12 : OhlcBase
    {
        public long? id_month { get; set; }
        public long? id_week { get; set; }
        public long? id_day { get; set; }

    }
}
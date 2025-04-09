using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities
{
    public abstract class OhlcBase
    {
        [Key]
        public long Id { get; set; }
        public long AssetId { get; set; }
        public string DataSource { get; set; }
        public string Broker { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal? Open { get; set; }
        public decimal? Close { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Co { get; set; }
        public decimal? Hl { get; set; }
        public decimal? TickVol { get; set; }
        public decimal? RealVol { get; set; }
        public DateTime? DateTime { get; set; }
        public string Direction { get; set; }
    }
}
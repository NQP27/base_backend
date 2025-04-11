using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities
{
    public abstract class OhlcBase
    {
        [Key]
        public long id { get; set; }
        public long asset_id { get; set; }
        public string data_source { get; set; }
        public string broker { get; set; }
        public string account_type { get; set; }
        public string account_name { get; set; }
        public decimal? open { get; set; }
        public decimal? close { get; set; }
        public decimal? high { get; set; }
        public decimal? low { get; set; }
        public decimal? co { get; set; }
        public decimal? hl { get; set; }
        public decimal? tick_vol { get; set; }
        public decimal? real_vol { get; set; }
        public DateTime? datetime { get; set; }
        public string direction { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities
{
    public class assets
    {
        [Key]
        public long id { get; set; }
        public string? symbol { get; set; }
        public string? prename { get; set; }
        public string? type { get; set; }
        public TimeSpan? us_open { get; set; }
        public TimeSpan? us_close { get; set; }
        public TimeSpan? asia_open { get; set; }
        public TimeSpan? asia_close { get; set; }
        public TimeSpan? eu_open { get; set; }
        public TimeSpan? eu_close { get; set; }
        public TimeSpan? sydney_open { get; set; }
        public TimeSpan? sydney_close { get; set; }
        public DateTime? season_ref { get; set; }
    }
}
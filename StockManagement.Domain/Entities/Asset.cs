using System;
using System.Collections.Generic;

namespace StockManagement.Domain.Entities
{
    public class Asset
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public string Prename { get; set; }
        public string Type { get; set; }
        public TimeSpan? UsOpen { get; set; }
        public TimeSpan? UsClose { get; set; }
        public TimeSpan? AsiaOpen { get; set; }
        public TimeSpan? AsiaClose { get; set; }
        public TimeSpan? EuOpen { get; set; }
        public TimeSpan? EuClose { get; set; }
        public TimeSpan? SydneyOpen { get; set; }
        public TimeSpan? SydneyClose { get; set; }
        public DateTime? SeasonRef { get; set; }

        // Navigation properties
        public ICollection<OhlcDay> OhlcDays { get; set; }
        public ICollection<OhlcH1> OhlcH1s { get; set; }
        public ICollection<OhlcH12> OhlcH12s { get; set; }
        public ICollection<OhlcH4> OhlcH4s { get; set; }
        public ICollection<OhlcM1> OhlcM1s { get; set; }
        public ICollection<OhlcM15> OhlcM15s { get; set; }
        public ICollection<OhlcM30> OhlcM30s { get; set; }
        public ICollection<OhlcM5> OhlcM5s { get; set; }
        public ICollection<OhlcMonth> OhlcMonths { get; set; }
        public ICollection<OhlcWeek> OhlcWeeks { get; set; }
    }
} 
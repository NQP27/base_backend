using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<assets> assets { get; set; }
        public DbSet<ohlc_day> ohlc_day { get; set; }
        public DbSet<ohlc_h1> ohlc_h1 { get; set; }
        public DbSet<ohlc_h12> ohlc_h12 { get; set; }
        public DbSet<ohlc_h4> ohlc_h4 { get; set; }
        public DbSet<ohlc_m1> ohlc_m1 { get; set; }
        public DbSet<ohlc_m15> ohlc_m15 { get; set; }
        public DbSet<ohlc_m30> ohlc_m30 { get; set; }
        public DbSet<ohlc_m5> ohlc_m5 { get; set; }
        public DbSet<ohlc_month> ohlc_month { get; set; }
        public DbSet<ohlc_week> ohlc_week { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}

using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<Asset> Asset { get; set; }
        public DbSet<OhlcDay> OhlcDay { get; set; }
        public DbSet<OhlcH1> OhlcH1 { get; set; }
        public DbSet<OhlcH12> OhlcH12 { get; set; }
        public DbSet<OhlcH4> OhlcH4 { get; set; }
        public DbSet<OhlcM1> OhlcM1 { get; set; }
        public DbSet<OhlcM15> OhlcM15 { get; set; }
        public DbSet<OhlcM30> OhlcM30 { get; set; }
        public DbSet<OhlcM5> OhlcM5 { get; set; }
        public DbSet<OhlcMonth> OhlcMonth { get; set; }
        public DbSet<OhlcWeek> OhlcWeek { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}

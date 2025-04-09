using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Interfaces.Context;

namespace StockManagement.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dwh");
            // Cấu hình các bảng business
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Cấu hình Identity
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(200);
            });
        }
    }
}
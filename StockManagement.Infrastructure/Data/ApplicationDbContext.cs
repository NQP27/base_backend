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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dwh");

            // Cấu hình relationship cho tất cả các bảng Ohlc
            //void ConfigureOhlcRelationship<T>(ModelBuilder builder) where T : class
            //{
            //    builder.Entity<T>()
            //        .HasOne("asset")
            //        .WithMany()
            //        .HasForeignKey("asset_id")
            //        .HasPrincipalKey("id");
            //}
            //modelBuilder.Entity<ohlc_m1>()
            //        .HasOne(x => x.asset)
            //        .WithMany()
            //        .HasForeignKey(x => x.asset_id)
            //        .HasPrincipalKey(o => o.id);

            ////ConfigureOhlcRelationship<ohlc_m1>(modelBuilder);
            //ConfigureOhlcRelationship<ohlc_m5>(modelBuilder);
            //ConfigureOhlcRelationship<ohlc_m15>(modelBuilder);
            //ConfigureOhlcRelationship<ohlc_m30>(modelBuilder);
            //ConfigureOhlcRelationship<ohlc_h1>(modelBuilder);
            //ConfigureOhlcRelationship<ohlc_h4>(modelBuilder);
            //ConfigureOhlcRelationship<ohlc_h12>(modelBuilder);
            //ConfigureOhlcRelationship<ohlc_day>(modelBuilder);
            //ConfigureOhlcRelationship<ohlc_week>(modelBuilder);
            //ConfigureOhlcRelationship<ohlc_month>(modelBuilder);

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
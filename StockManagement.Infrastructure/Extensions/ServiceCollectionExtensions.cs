using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockManagement.Domain.Interfaces.Context;
using StockManagement.Domain.Interfaces.Repositories;
using StockManagement.Infrastructure.Data;
using StockManagement.Infrastructure.Persistences.Repositories;

namespace StockManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IAuthenticationRepository), typeof(AuthenticationRepository));
            services.AddScoped(typeof(IOhlcRepository), typeof(OhlcRepository));
            services.AddScoped(typeof(IApplicationDbContext), typeof(ApplicationDbContext));

            return services;
        }
    }
}
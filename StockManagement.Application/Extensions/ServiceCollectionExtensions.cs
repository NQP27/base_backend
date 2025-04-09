using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockManagement.Application.Configurations.AutoMapper;
using StockManagement.Application.Services;
using StockManagement.Domain.Interfaces.Services;

namespace BeLight.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAplication(this IServiceCollection services, IConfiguration config)
        {
            // Register Automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // Register MediatR
            //services.AddMediatR(cfg =>
            //{
            //    //cfg.RegisterServicesFromAssemblies(typeof(CreateLocationCommand).Assembly);
            //});

            services.AddTransient<IJwtService, JwtService>();

            //services.AddSingleton<IEmailService>(provider =>
            //{
            //    var email = config["EmailService:Email"]; // Lấy địa chỉ email từ appsettings
            //    var password = config["EmailService:Password"]; // Lấy mật khẩu từ appsettings
            //    return new EmailService(email, password);
            //});

            return services;
        }
    }
}
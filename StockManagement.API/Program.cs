using BeLight.Application.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using StockManagement.API.Configurations;
using StockManagement.Infrastructure.Data;
using StockManagement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ProducesAttribute("application/json"));
    opt.Filters.Add(new ConsumesAttribute("application/json"));

    // Authorization policy
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddEndpointsApiExplorer();

// Add Swagger
builder.Services.AddSwaggerModule();
builder.Services.AddInfrastructure(config);
builder.Services.AddAplication(config);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.CommandTimeout(120);
            sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
        }));


// Add Identity Module
builder.Services.AddIdentityModule(config);

// Add Google Authentication
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = config["Authentication:Google:ClientId"];
        options.ClientSecret = config["Authentication:Google:ClientSecret"];
        options.CallbackPath = "/api/authentication/signin-google";
        options.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        options.TokenEndpoint = "https://oauth2.googleapis.com/token";
        options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
    });

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseApplicationSwagger();
}

// Global Exception Handling
app.UseExceptionHandler("/error");

// Enable CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



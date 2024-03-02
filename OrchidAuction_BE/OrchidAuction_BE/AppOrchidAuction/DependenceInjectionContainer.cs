using BusinessLayer.Services;
using BusinessLayer.Services.Implements;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace OrchidAuction_BE.AppStarts
{
    public static class DependenceInjectionContainer
    {
        public static void InstallService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true; ;
                options.LowercaseQueryStrings = true;
            });
            services.AddDbContext<OrchidAuctionContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IEmailService, EmailService>();
        }
    }
}

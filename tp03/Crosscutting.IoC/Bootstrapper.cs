using Microsoft.Extensions.DependencyInjection;
using Data.Data;
using Data.Repositories;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Crosscutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MobileAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MobileAppDbContext")));

            services.AddTransient<IDeveloperService, DeveloperService>();
            services.AddTransient<IDeveloperRepository, DeveloperRepository>();
            services.AddTransient<IMobileAppService, MobileAppService>();
            services.AddTransient<IMobileAppRepository, MobileAppRepository>();
        }
    }
}

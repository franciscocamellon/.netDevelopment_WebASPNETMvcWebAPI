using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
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
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;

namespace Data.Data
{
    public class AppLibraryDbContext : DbContext
    {
        public AppLibraryDbContext (DbContextOptions<AppLibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Model.Models.DeveloperModel> DeveloperModel { get; set; }
        public DbSet<Domain.Model.Models.MobileAppModel> MobileAppModel { get; set; }
    }
}

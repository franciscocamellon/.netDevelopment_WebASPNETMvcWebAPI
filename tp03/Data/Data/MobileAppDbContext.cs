using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;

namespace Data.Data
{
    public class MobileAppDbContext : DbContext
    {
        public MobileAppDbContext (DbContextOptions<MobileAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Model.Models.DeveloperModel> Developers { get; set; }
        public DbSet<Domain.Model.Models.MobileAppModel> MobileApps { get; set; }
    }
}

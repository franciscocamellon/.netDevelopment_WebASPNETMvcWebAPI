using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Model;

namespace Data.Data
{
    public class MobileAppDbContext : DbContext
    {
        public MobileAppDbContext (DbContextOptions<MobileAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Model.Model.DeveloperModel> Developers { get; set; }
    }
}

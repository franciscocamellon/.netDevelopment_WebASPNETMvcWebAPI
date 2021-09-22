using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class MobileAppDbContext : DbContext
    {
        public MobileAppDbContext (DbContextOptions<MobileAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder
                .Entity<DeveloperModel>()
                .Ignore(x => x.PublishedApps);
        }

        public DbSet<Domain.Model.Models.DeveloperModel> Developers { get; set; }
        public DbSet<Domain.Model.Models.MobileAppModel> MobileApps { get; set; }
    }
}

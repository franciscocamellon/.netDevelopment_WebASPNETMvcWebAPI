using Microsoft.EntityFrameworkCore;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly MobileAppDbContext _mobileAppDbContext;
        public DeveloperRepository(
            MobileAppDbContext mobileAppDbContext)
        {
            _mobileAppDbContext = mobileAppDbContext;
        }

        public async Task<IEnumerable<DeveloperModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var developers = orderAscendant
                ? _mobileAppDbContext.Developers.OrderBy(x => x.FirstName)
                : _mobileAppDbContext.Developers.OrderByDescending(x => x.FirstName);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await developers.ToListAsync();
            }

            return await developers
                .Where(x=> x.FirstName.Contains(search) || x.LastName.Contains(search))
                .ToListAsync();
        }

        public async Task<DeveloperModel> GetByIdAsync(Guid id)
        {
            var developer = await _mobileAppDbContext
                .Developers
                .Include(x => x.MobileApps)
                .FirstOrDefaultAsync(x => x.Id == id);

            return developer;
        }

        public async Task<DeveloperModel> CreateAsync(DeveloperModel developerModel)
        {
            var developer = _mobileAppDbContext.Developers.Add(developerModel);

            await _mobileAppDbContext.SaveChangesAsync();

            return developer.Entity;
        }

        public async Task<DeveloperModel> EditAsync(DeveloperModel developerModel)
        {
            var developer = _mobileAppDbContext.Developers.Update(developerModel);

            await _mobileAppDbContext.SaveChangesAsync();

            return developer.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var developer = await GetByIdAsync(id);

            _mobileAppDbContext.Developers.Remove(developer);

            await _mobileAppDbContext.SaveChangesAsync();
        }
    }
}

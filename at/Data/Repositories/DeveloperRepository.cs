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
            var developers = _mobileAppDbContext.Developers.AsQueryable();

            if (string.IsNullOrWhiteSpace(search))
            {
                developers = developers
                    .Where(x=> x.FirstName.Contains(search) || x.LastName.Contains(search));
            }

            developers = orderAscendant
                ? _mobileAppDbContext.Developers.OrderBy(x => x.FirstName)
                : _mobileAppDbContext.Developers.OrderByDescending(x => x.FirstName);

            var result = await developers
                .Select(x => new
                {
                    Developer = x,
                    PublishedApps = x.MobileApps.Count
                })
                .ToListAsync();

            var developerResult = result
                .Select(x =>
                {
                    x.Developer.PublishedApps = x.PublishedApps;
                    return x.Developer;
                });

            return developerResult;
        }

        public async Task<DeveloperModel> GetByIdAsync(Guid id)
        {
            var developerTask = _mobileAppDbContext
                .Developers
                .Include(x => x.MobileApps)
                .FirstOrDefaultAsync(x => x.Id == id);

            var publishedApps = _mobileAppDbContext.MobileApps.CountAsync(x => x.DeveloperId == id);

            await Task.WhenAll(developerTask, publishedApps);

            var developer = await developerTask;

            developer.PublishedApps = await publishedApps;

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

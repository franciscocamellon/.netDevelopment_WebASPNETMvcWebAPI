using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class MobileAppRepository : IMobileAppRepository
    {
        private readonly MobileAppDbContext _mobileAppDbContext;
        public MobileAppRepository(
            MobileAppDbContext mobileAppDbContext)
        {
            _mobileAppDbContext = mobileAppDbContext;
        }

        public async Task<IEnumerable<MobileAppModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var mobileApps = orderAscendant
                ? _mobileAppDbContext.MobileApps.OrderBy(x => x.AppName)
                : _mobileAppDbContext.MobileApps.OrderByDescending(x => x.AppName);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await mobileApps
                    .Include(x => x.Developer)
                    .ToListAsync();
            }

            return await mobileApps
                .Include(x => x.Developer)
                .Where(x => x.AppName.Contains(search))
                .ToListAsync();
        }

        public async Task<MobileAppModel> GetByIdAsync(Guid id)
        {
            var mobileApp = await _mobileAppDbContext
                .MobileApps
                .Include(x => x.Developer)
                .FirstOrDefaultAsync(x => x.Id == id);

            return mobileApp;
        }

        public async Task<MobileAppModel> CreateAsync(MobileAppModel mobileAppModel)
        {
            var mobileApp = _mobileAppDbContext.MobileApps.Add(mobileAppModel);

            await _mobileAppDbContext.SaveChangesAsync();

            return mobileApp.Entity;
        }

        public async Task<MobileAppModel> EditAsync(MobileAppModel mobileAppModel)
        {
            var mobileApp = _mobileAppDbContext.MobileApps.Update(mobileAppModel);

            await _mobileAppDbContext.SaveChangesAsync();

            return mobileApp.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var mobileApp = await GetByIdAsync(id);

            _mobileAppDbContext.MobileApps.Remove(mobileApp);

            await _mobileAppDbContext.SaveChangesAsync();
        }

        public async Task<MobileAppModel> GetNameNotFromThisIdAsync(string appName, Guid id)
        {
            var mobileAppModel = await _mobileAppDbContext
                .MobileApps
                .FirstOrDefaultAsync(x => x.AppName == appName && x.Id != id);

            return mobileAppModel;
        }
    }
}

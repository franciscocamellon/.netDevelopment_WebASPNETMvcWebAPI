using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services.Implementations
{
    public class MobileAppFakeService : IMobileAppHttpService
    {
        private static List<MobileAppViewModel> MobileApps { get; } = new List<MobileAppViewModel>
        {
            new MobileAppViewModel
            {
                Id = new Guid(),
                AppName = "App Test",
                PublishedStatus = false,
                PublishedDate = new DateTime(2002, 06, 15),
                ModificationDate = new DateTime(2002, 06, 15)
            },
            new MobileAppViewModel
            {
                Id = new Guid(),
                AppName = "App Test Two",
                PublishedStatus = false,
                PublishedDate = new DateTime(2002, 06, 15),
                ModificationDate = new DateTime(2002, 06, 15)
            },
        };

        public async Task<IEnumerable<MobileAppViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            if (search == null)
            {
                return MobileApps;
            }

            var resultByLinq = MobileApps
                .Where(x =>
                    x.AppName.Contains(search, StringComparison.OrdinalIgnoreCase));

            resultByLinq = orderAscendant
                ? resultByLinq.OrderBy(x => x.AppName)
                : resultByLinq.OrderByDescending(x => x.AppName);

            return resultByLinq;
        }

        public async Task<MobileAppViewModel> GetByIdAsync(Guid id)
        {
            foreach (var mobileApp in MobileApps)
            {
                if (mobileApp.Id == id)
                {
                    return mobileApp;
                }
            }

            return null;
        }

        public async Task<MobileAppViewModel> CreateAsync(MobileAppViewModel mobileAppViewModel)
        {
            mobileAppViewModel.Id = Guid.NewGuid();

            MobileApps.Add(mobileAppViewModel);

            //TODO: auto-increment Id e atualizar para o retorno
            return mobileAppViewModel;
        }

        public async Task<MobileAppViewModel> EditAsync(MobileAppViewModel mobileAppViewModel)
        {
            foreach (var mobileApp in MobileApps)
            {
                if (mobileApp.Id == mobileAppViewModel.Id)
                {
                    mobileApp.AppName = mobileAppViewModel.AppName;
                    mobileApp.PublishedStatus = mobileAppViewModel.PublishedStatus;
                    mobileApp.PublishedDate = mobileAppViewModel.PublishedDate;
                    mobileApp.ModificationDate = mobileAppViewModel.ModificationDate;
                    mobileApp.DeveloperId = mobileAppViewModel.DeveloperId;

                    return mobileApp;
                }
            }

            return null;
        }
        
        public async Task DeleteAsync(Guid id)
        {
            MobileAppViewModel foundedMobileApp = null;
            foreach (var mobileApp in MobileApps)
            {
                if (mobileApp.Id == id)
                {
                    foundedMobileApp = mobileApp;
                }
            }

            if (foundedMobileApp != null)
            {
                MobileApps.Remove(foundedMobileApp);
            }
        }

        public Task<bool> IsUnusedNameAsync(string appName, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

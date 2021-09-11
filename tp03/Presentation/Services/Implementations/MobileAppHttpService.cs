using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class MobileAppHttpService : IMobileAppHttpService
    {
        public async Task<IEnumerable<MobileAppViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            throw new NotImplementedException();
        }

        public async Task<MobileAppViewModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<MobileAppViewModel> CreateAsync(MobileAppViewModel mobileAppViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<MobileAppViewModel> EditAsync(MobileAppViewModel mobileAppViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsUnusedNameAsync(string appName, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

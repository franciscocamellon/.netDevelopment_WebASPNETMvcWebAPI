using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services
{
    public interface IMobileAppHttpService
    {
        Task<IEnumerable<MobileAppViewModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<MobileAppViewModel> GetByIdAsync(Guid id);
        Task<MobileAppViewModel> CreateAsync(MobileAppViewModel mobileAppViewModel);
        Task<MobileAppViewModel> EditAsync(MobileAppViewModel mobileAppViewModel);
        Task DeleteAsync(Guid id);
        Task<bool> IsUnusedNameAsync(string appName, Guid id);
    }   
}

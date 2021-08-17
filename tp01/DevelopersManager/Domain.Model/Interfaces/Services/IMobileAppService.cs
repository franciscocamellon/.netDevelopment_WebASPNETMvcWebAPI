using Domain.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    interface IMobileAppService
    {
        Task<IEnumerable<MobileAppModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<MobileAppModel> GetByIdAsync(string id);
        Task<MobileAppModel> CreateAsync(MobileAppModel mobileAppModel);
        Task<MobileAppModel> EditAsync(MobileAppModel mobileAppModel);
        Task DeleteAsync(string id);
    }
}

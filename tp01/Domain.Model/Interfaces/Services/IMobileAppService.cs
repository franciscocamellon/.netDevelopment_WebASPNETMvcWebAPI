using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    public interface IMobileAppService
    {
        Task<IEnumerable<MobileAppModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<MobileAppModel> GetByIdAsync(Guid id);
        Task<MobileAppModel> CreateAsync(MobileAppModel mobileAppModel);
        Task<MobileAppModel> EditAsync(MobileAppModel mobileAppModel);
        Task DeleteAsync(Guid id);
    }
}

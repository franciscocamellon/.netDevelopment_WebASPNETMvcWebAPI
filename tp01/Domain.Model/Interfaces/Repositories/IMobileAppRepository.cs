using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IMobileAppRepository
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

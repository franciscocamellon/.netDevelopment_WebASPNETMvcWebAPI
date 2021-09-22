using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services
{
    public interface IDeveloperHttpService
    {
        Task<IEnumerable<DeveloperViewModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<DeveloperViewModel> GetByIdAsync(Guid id);
        Task<DeveloperViewModel> CreateAsync(DeveloperViewModel developerModel);
        Task<DeveloperViewModel> EditAsync(DeveloperViewModel developerModel);
        Task DeleteAsync(Guid id);
    }
}

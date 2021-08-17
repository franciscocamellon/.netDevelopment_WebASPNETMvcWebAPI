using Domain.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    interface IDeveloperService
    {
        Task<IEnumerable<DeveloperModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<DeveloperModel> GetByIdAsync(string id);
        Task<DeveloperModel> CreateAsync(DeveloperModel developerModel);
        Task<DeveloperModel> EditAsync(DeveloperModel developerModel);
        Task DeleteAsync(string id);
    }
}

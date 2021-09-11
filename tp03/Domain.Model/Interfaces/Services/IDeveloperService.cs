using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<DeveloperModel> GetByIdAsync(Guid id);
        Task<DeveloperModel> CreateAsync(DeveloperModel developerModel);
        Task<DeveloperModel> EditAsync(DeveloperModel developerModel);
        Task DeleteAsync(Guid id);
    }
}

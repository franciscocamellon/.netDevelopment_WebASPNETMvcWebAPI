using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IDeveloperRepository
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
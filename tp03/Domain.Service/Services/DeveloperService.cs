using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperService(
            IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }
        public async Task<IEnumerable<DeveloperModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _developerRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<DeveloperModel> GetByIdAsync(Guid id)
        {
            return await _developerRepository.GetByIdAsync(id);
        }

        public async Task<DeveloperModel> CreateAsync(DeveloperModel developerModel)
        {
            return await _developerRepository.CreateAsync(developerModel);
        }

        public async Task<DeveloperModel> EditAsync(DeveloperModel developerModel)
        {
            return await _developerRepository.EditAsync(developerModel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _developerRepository.DeleteAsync(id);
        }
    }
}

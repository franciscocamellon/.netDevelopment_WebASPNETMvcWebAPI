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
    public class MobileAppService : IMobileAppService
    {
        private readonly IMobileAppRepository _developerRepository;

        public MobileAppService(
            IMobileAppRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }
        public async Task<IEnumerable<MobileAppModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _developerRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<MobileAppModel> GetByIdAsync(Guid id)
        {
            return await _developerRepository.GetByIdAsync(id);
        }

        public async Task<MobileAppModel> CreateAsync(MobileAppModel mobileAppModel)
        {
            return await _developerRepository.CreateAsync(mobileAppModel);
        }

        public async Task<MobileAppModel> EditAsync(MobileAppModel mobileAppModel)
        {
            return await _developerRepository.EditAsync(mobileAppModel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _developerRepository.DeleteAsync(id);
        }
    }
}

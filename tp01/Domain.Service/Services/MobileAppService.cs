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
        private readonly IMobileAppRepository _mobileAppRepository;

        public MobileAppService(
            IMobileAppRepository mobileAppRepository)
        {
            _mobileAppRepository = mobileAppRepository;
        }
        public async Task<IEnumerable<MobileAppModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _mobileAppRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<MobileAppModel> GetByIdAsync(Guid id)
        {
            return await _mobileAppRepository.GetByIdAsync(id);
        }

        public async Task<MobileAppModel> CreateAsync(MobileAppModel mobileAppModel)
        {
            return await _mobileAppRepository.CreateAsync(mobileAppModel);
        }

        public async Task<MobileAppModel> EditAsync(MobileAppModel mobileAppModel)
        {
            return await _mobileAppRepository.EditAsync(mobileAppModel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mobileAppRepository.DeleteAsync(id);
        }

        public async Task<bool> IsUnusedNameAsync(string appName, Guid id)
        {
            var mobileAppModel = await _mobileAppRepository.GetNameNotFromThisIdAsync(appName, id);

            return mobileAppModel == null;
        }
    }
}

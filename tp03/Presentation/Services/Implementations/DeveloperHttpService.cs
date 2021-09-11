using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class DeveloperHttpService : IDeveloperHttpService
    {
        public async Task<IEnumerable<DeveloperViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            throw new NotImplementedException();
        }

        public async Task<DeveloperViewModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<DeveloperViewModel> CreateAsync(DeveloperViewModel developerModel)
        {
            throw new NotImplementedException();
        }

        public async Task<DeveloperViewModel> EditAsync(DeveloperViewModel developerModel)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

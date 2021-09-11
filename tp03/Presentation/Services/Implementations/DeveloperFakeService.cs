using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Services.Implementations
{
    public class DeveloperFakeService : IDeveloperHttpService
    {
        private static List<DeveloperViewModel> Developers { get; } = new List<DeveloperViewModel>
        {
            new DeveloperViewModel
            {
                Id = new Guid(),
                FirstName = "Mateusz",
                LastName = "Solis",
                GraduationDate = new DateTime(2002, 06, 15),
                EmployedStatus = false,
                PublishedApps = 2,
            },
            new DeveloperViewModel
            {
                Id = new Guid(),
                FirstName = "Van",
                LastName = "Helsing",
                GraduationDate = new DateTime(2012, 09, 25),
                EmployedStatus = false,
                PublishedApps = 2,
            },
        };

        public async Task<IEnumerable<DeveloperViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            if (search == null)
            {
                return Developers;
            }

            var resultByLinq = Developers
                .Where(x =>
                    x.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    x.LastName.Contains(search, StringComparison.OrdinalIgnoreCase));

            resultByLinq = orderAscendant
                ? resultByLinq.OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                : resultByLinq.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName);

            return resultByLinq;
        }

        public async Task<DeveloperViewModel> GetByIdAsync(Guid id)
        {
            foreach (var developer in Developers)
            {
                if (developer.Id == id)
                {
                    return developer;
                }
            }

            return null;
        }

        public async Task<DeveloperViewModel> CreateAsync(DeveloperViewModel developerViewModel)
        {
            developerViewModel.Id = Guid.NewGuid();

            Developers.Add(developerViewModel);

            //TODO: auto-increment Id e atualizar para o retorno
            return developerViewModel;
        }

        public async Task<DeveloperViewModel> EditAsync(DeveloperViewModel developerViewModel)
        {
            foreach (var developer in Developers)
            {
                if (developer.Id == developerViewModel.Id)
                {
                    developer.FirstName = developerViewModel.FirstName;
                    developer.LastName = developerViewModel.LastName;
                    developer.GraduationDate = developerViewModel.GraduationDate;
                    developer.EmployedStatus = developerViewModel.EmployedStatus;
                    developer.PublishedApps = developerViewModel.PublishedApps;

                    return developer;
                }
            }

            return null;
        }
        
        public async Task DeleteAsync(Guid id)
        {
            DeveloperViewModel foundedDeveloper = null;
            foreach (var developer in Developers)
            {
                if (developer.Id == id)
                {
                    foundedDeveloper = developer;
                }
            }

            if (foundedDeveloper != null)
            {
                Developers.Remove(foundedDeveloper);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class DeveloperHttpService : IDeveloperHttpService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        public DeveloperHttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44350/");
        }
        public async Task<IEnumerable<DeveloperViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var developers = await _httpClient
                .GetFromJsonAsync<IEnumerable<DeveloperViewModel>>("/api/v1/DeveloperApi/");

            return developers;
        }

        public async Task<DeveloperViewModel> GetByIdAsync(Guid id)
        {
            var developer = await _httpClient
                .GetFromJsonAsync<DeveloperViewModel>($"/api/v1/DeveloperApi/{id}");

            return developer;
        }

        public async Task<DeveloperViewModel> CreateAsync(DeveloperViewModel developerViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync("/api/v1/DeveloperApi", developerViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var createdDeveloper = await JsonSerializer
                .DeserializeAsync<DeveloperViewModel>(contentStream, JsonSerializerOptions);

            return createdDeveloper;
        }

        public async Task<DeveloperViewModel> EditAsync(DeveloperViewModel developerViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"/api/v1/DeveloperApi/{developerViewModel.Id}", developerViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var editedDeveloper = await JsonSerializer
                .DeserializeAsync<DeveloperViewModel>(contentStream, JsonSerializerOptions);

            return editedDeveloper;
        }

        public async Task DeleteAsync(Guid id)
        {
            var httpResponseMessage = await _httpClient
                .DeleteAsync($"/api/v1/DeveloperApi/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}

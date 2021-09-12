using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class MobileAppHttpService : IMobileAppHttpService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        public MobileAppHttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44350/");
        }
        public async Task<IEnumerable<MobileAppViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var mobileApps = await _httpClient
                .GetFromJsonAsync<IEnumerable<MobileAppViewModel>>("/api/v1/MobileAppApi/");

            return mobileApps;
        }

        public async Task<MobileAppViewModel> GetByIdAsync(Guid id)
        {
            var mobileApp = await _httpClient
                .GetFromJsonAsync<MobileAppViewModel>($"/api/v1/MobileAppApi/{id}");

            return mobileApp;
        }

        public async Task<MobileAppViewModel> CreateAsync(MobileAppViewModel mobileAppViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync("/api/v1/MobileAppApi", mobileAppViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var createdMobileApp = await JsonSerializer
                .DeserializeAsync<MobileAppViewModel>(contentStream, JsonSerializerOptions);

            return createdMobileApp;
        }

        public async Task<MobileAppViewModel> EditAsync(MobileAppViewModel mobileAppViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"/api/v1/MobileAppApi/{mobileAppViewModel.Id}", mobileAppViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var editedMobileApp = await JsonSerializer
                .DeserializeAsync<MobileAppViewModel>(contentStream, JsonSerializerOptions);

            return editedMobileApp;
        }

        public async Task DeleteAsync(Guid id)
        {
            var httpResponseMessage = await _httpClient
                .DeleteAsync($"/api/v1/MobileAppApi/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<bool> IsUnusedNameAsync(string appName, Guid id)
        {
            var isUsed = await _httpClient
                .GetFromJsonAsync<bool>($"/api/v1/MobileAppApi/IsUnusedName/{appName}/{id}");

            return isUsed;
        }
    }
}

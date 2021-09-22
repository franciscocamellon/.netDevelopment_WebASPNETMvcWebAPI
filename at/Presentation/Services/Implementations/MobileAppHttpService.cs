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

        public MobileAppHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<MobileAppViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var mobileApps = await _httpClient
                .GetFromJsonAsync<IEnumerable<MobileAppViewModel>>($"{orderAscendant}/{search}");

            return mobileApps;
        }

        public async Task<MobileAppViewModel> GetByIdAsync(Guid id)
        {
            var mobileApp = await _httpClient
                .GetFromJsonAsync<MobileAppViewModel>($"{id}");

            return mobileApp;
        }

        public async Task<MobileAppViewModel> CreateAsync(MobileAppViewModel mobileAppViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync(string.Empty, mobileAppViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var createdMobileApp = await JsonSerializer
                .DeserializeAsync<MobileAppViewModel>(contentStream, JsonSerializerOptions);

            return createdMobileApp;
        }

        public async Task<MobileAppViewModel> EditAsync(MobileAppViewModel mobileAppViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"{mobileAppViewModel.Id}", mobileAppViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var editedMobileApp = await JsonSerializer
                .DeserializeAsync<MobileAppViewModel>(contentStream, JsonSerializerOptions);

            return editedMobileApp;
        }

        public async Task DeleteAsync(Guid id)
        {
            var httpResponseMessage = await _httpClient
                .DeleteAsync($"{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<bool> IsUnusedNameAsync(string appName, Guid id)
        {
            var isUsed = await _httpClient
                .GetFromJsonAsync<bool>($"IsUnusedName/{appName}/{id}");

            return isUsed;
        }
    }
}

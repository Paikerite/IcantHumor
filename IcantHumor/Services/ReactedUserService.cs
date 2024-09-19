using IcantHumor.Models;
using IcantHumor.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace IcantHumor.Services
{
    public class ReactedUserService : IReactedUserService
    {
        private readonly HttpClient httpClient;
        public ReactedUserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ReactedUserViewModel?> DeleteReactedUser(Guid id)
        {
            var response = await httpClient.DeleteAsync($"api/ReactedUser/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReactedUserViewModel>();
            }
            return default(ReactedUserViewModel);
        }

        public async Task<ReactedUserViewModel?> GetReactedUser(Guid id)
        {
            var response = await this.httpClient.GetAsync($"api/ReactedUser/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReactedUserViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(ReactedUserViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<ReactedUserViewModel>?> GetReactedUsers()
        {
            var products = await this.httpClient.GetAsync("api/ReactedUser");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<ReactedUserViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<ReactedUserViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<ReactedUserViewModel?> PostReactedUser(ReactedUserViewModel reactedUserViewModel)
        {
            var response = await httpClient.PostAsJsonAsync<ReactedUserViewModel>("api/ReactedUser", reactedUserViewModel);
            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<ReactedUserViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(ReactedUserViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<ReactedUserViewModel?> PutReactedUser(Guid id, ReactedUserViewModel reactedUserViewModel)
        {
            var JsonRequest = JsonConvert.SerializeObject(reactedUserViewModel);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PutAsync($"api/ReactedUser/{id}", content);
            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<ReactedUserViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return default(ReactedUserViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }
    }
}

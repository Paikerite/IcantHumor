using IcantHumor.Models;
using IcantHumor.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace IcantHumor.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient httpClient;
        public CategoriesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CategoryViewModel> DeleteCategory(Guid id)
        {
            var response = await httpClient.DeleteAsync($"api/Categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            }
            return default(CategoryViewModel);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            var products = await this.httpClient.GetAsync("api/Categories");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<CategoryViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<CategoryViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<CategoryViewModel> GetCategory(Guid id)
        {
            var response = await this.httpClient.GetAsync($"api/Categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(CategoryViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<CategoryViewModel> GetCategoryByName(string name)
        {
            var response = await this.httpClient.GetAsync($"api/Categories/GetCategoryByName/{name}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(CategoryViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<CategoryViewModel> PostCategory(CategoryViewModel categoryViewModel)
        {
            var response = await httpClient.PostAsJsonAsync<CategoryViewModel>("api/Categories", categoryViewModel);
            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(CategoryViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<CategoryViewModel> PutCategory(Guid id, CategoryViewModel categoryViewModel)
        {
            var JsonRequest = JsonConvert.SerializeObject(categoryViewModel);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PutAsync($"api/Categories/{id}", content);
            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return default(CategoryViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }
    }
}

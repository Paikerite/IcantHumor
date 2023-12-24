using IcantHumor.Models;
using IcantHumor.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace IcantHumor.Services
{
    public class FavouritesService : IFavouritesService
    {
        private readonly HttpClient httpClient;

        public FavouritesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<FavouriteViewModel> DeleteFavourite(Guid id)
        {
            var response = await httpClient.DeleteAsync($"api/Favourites/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<FavouriteViewModel>();
            }
            return default(FavouriteViewModel);
        }

        public async Task<IEnumerable<FavouriteViewModel>> GetFavourites()
        {
            var products = await this.httpClient.GetAsync("api/Favourites");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<FavouriteViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<FavouriteViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<FavouriteViewModel> GetFavourite(Guid id)
        {
            var response = await this.httpClient.GetAsync($"api/Favourites/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<FavouriteViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(FavouriteViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<FavouriteViewModel> PostFavourite(FavouriteViewModel favouriteViewModel)
        {
            var response = await httpClient.PostAsJsonAsync<FavouriteViewModel>("api/Favourites", favouriteViewModel);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<FavouriteViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(FavouriteViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<FavouriteViewModel> PutFavourite(Guid id, FavouriteViewModel favouriteViewModel)
        {
            var JsonRequest = JsonConvert.SerializeObject(favouriteViewModel);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PutAsync($"api/Favourites/{id}", content);
            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<FavouriteViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return default(FavouriteViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<FavouriteViewModel>> GetFavouritesByUser(Guid idUser)
        {
            var products = await this.httpClient.GetAsync($"api/Favourites/GetFavouritesByUser/{idUser}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<FavouriteViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<FavouriteViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<FavouriteViewModel>> GetCountFavouritesByUser(Guid idUser, int countFav)
        {
            var products = await this.httpClient.GetAsync($"api/Favourites/GetCountFavouritesByUser/{idUser}/{countFav}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<FavouriteViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<FavouriteViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }
    }
}

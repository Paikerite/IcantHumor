using IcantHumor.Models;
using IcantHumor.Models.Enums;
using IcantHumor.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace IcantHumor.Services
{
    public class MediaFilesService : IMediaFilesService
    {
        private readonly HttpClient httpClient;
        public MediaFilesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<MediaViewModel?> DeleteMediaViewModel(Guid id)
        {
            var response = await httpClient.DeleteAsync($"api/MediaFiles/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MediaViewModel>();
            }
            return default(MediaViewModel);
        }

        public async Task<IEnumerable<MediaViewModel>?> DeleteAllMediaByCreatedUserId(Guid guid)
        {
            var products = await this.httpClient.DeleteAsync($"api/MediaFiles/DeleteAllMediaByCreatedUserId/{guid}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<MediaViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<MediaViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<MediaViewModel>?> GetMediaFiles()
        {
            var products = await this.httpClient.GetAsync("api/MediaFiles");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<MediaViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<MediaViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<MediaViewModel>?> GetMediaFilesByCategories(IEnumerable<Guid> categories)
        {
            string content = string.Join("&", categories);

            var products = await this.httpClient.GetAsync($"api/MediaFiles/GetMediaFilesByCategories/{content}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<MediaViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<MediaViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<MediaViewModel>?> GetCategorizedMediaPerPage(int page, int itemsPerPage, IEnumerable<Guid> categoriesIds, Sort sorting)
        {
            string content = string.Join("&", categoriesIds);

            var products = await this.httpClient.GetAsync($"api/MediaFiles/GetCategorizedMediaPerPage/{page}/{itemsPerPage}/{content}/{sorting}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<MediaViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<MediaViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<MediaViewModel>?> GetMediaPerPage(int page, int itemsPerPage, Sort sorting)
        {
            var products = await this.httpClient.GetAsync($"api/MediaFiles/GetMediaPerPage/{page}/{itemsPerPage}/{sorting}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<MediaViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<MediaViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<MediaViewModel>?> GetMediaFilesByCategory(Guid categoryId)
        {
            var response = await httpClient.GetAsync($"api/MediaFiles/GetMediaFilesByCategory/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<MediaViewModel>();
                }
                return await response.Content.ReadFromJsonAsync<IEnumerable<MediaViewModel>>();

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<MediaViewModel>?> GetMediaFilesByName(string SearchText)
        {
            var products = await this.httpClient.GetAsync($"api/MediaFiles/GetMediaFilesByName/{SearchText}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<MediaViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<MediaViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<MediaViewModel?> GetMediaViewModel(Guid id)
        {
            var response = await this.httpClient.GetAsync($"api/MediaFiles/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MediaViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(MediaViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<MediaViewModel?> MakeReactionInPost(Guid idPost, ReactedUserViewModel reactedUser)
        {
            var JsonRequest = JsonConvert.SerializeObject(reactedUser);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PatchAsync($"api/MediaFiles/MakeReactionInPost/{idPost}", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MediaViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return default(MediaViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<MediaViewModel?> PatchCategoryInPost(Guid idPost, IEnumerable<Guid> categoriesIds)
        {
            var JsonRequest = JsonConvert.SerializeObject(categoriesIds);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PatchAsync($"api/MediaFiles/PatchCategoryInPost/{idPost}", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MediaViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return default(MediaViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<MediaViewModel?> PostMediaViewModel(MediaViewModel mediaViewModel)
        {
            var response = await httpClient.PostAsJsonAsync<MediaViewModel>("api/MediaFiles", mediaViewModel);
            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<MediaViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(MediaViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<MediaViewModel?> PutMediaViewModel(Guid id, MediaViewModel mediaViewModel)
        {
            var JsonRequest = JsonConvert.SerializeObject(mediaViewModel);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PutAsync($"api/MediaFiles/{id}", content);
            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<MediaViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return default(MediaViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<MediaViewModel?> UnMakeReactionInPost(Guid idPost, ReactedUserViewModel reactedUser)
        {
            var JsonRequest = JsonConvert.SerializeObject(reactedUser);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PatchAsync($"api/MediaFiles/UnMakeReactionInPost/{idPost}", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MediaViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return default(MediaViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<int> GetCountMediaFiles()
        {
            var products = await this.httpClient.GetAsync("api/MediaFiles/GetCountMediaFiles");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<int>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 0;
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<int> GetCountMediaFilesIncludeCategories(IEnumerable<Guid> categoriesIds)
        {
            string content = string.Join("&", categoriesIds);
            var products = await this.httpClient.GetAsync($"api/MediaFiles/GetCountMediaFilesIncludeCategories/{content}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<int>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 0;
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<IEnumerable<MediaViewModel>?> GetMediaFilesByNameByPages(string SearchText, int page, int itemsPerPage, Sort sorting)
        {
            var products = await this.httpClient.GetAsync($"api/MediaFiles/GetMediaFilesByNameByPages/{SearchText}/{page}/{itemsPerPage}/{sorting}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<IEnumerable<MediaViewModel>>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Enumerable.Empty<MediaViewModel>();
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<int> GetCountMediaFilesBySearch(string SearchText)
        {
            var products = await this.httpClient.GetAsync($"api/MediaFiles/GetCountMediaFilesBySearch/{SearchText}");
            if (products.IsSuccessStatusCode)
            {
                return await products.Content.ReadFromJsonAsync<int>();
            }
            else if (products.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 0;
            }
            else
            {
                var message = await products.Content.ReadAsStringAsync();
                throw new Exception($"http status:{products.StatusCode}, message:{message}");
            }
        }

        public async Task<MediaViewModel?> UpdateTitlePost(Guid idPost, string title)
        {
            UpdateTitleMediaFiles model = new() { NewTitle = title };
            var JsonRequest = JsonConvert.SerializeObject(model);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PatchAsync($"api/MediaFiles/UpdateTitlePost/{idPost}", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MediaViewModel>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return default(MediaViewModel);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }
    }
}

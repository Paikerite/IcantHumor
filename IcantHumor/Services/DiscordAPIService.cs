using IcantHumor.Models;
using IcantHumor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace IcantHumor.Services
{
    public class DiscordAPIService : IDiscordAPIService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;
        private readonly IOptions<OAuthDiscordConfig> oauthConfig;

        public DiscordAPIService(HttpClient httpClient, NavigationManager navigationManager, IOptions<OAuthDiscordConfig> oauthConfig)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
            this.oauthConfig = oauthConfig;
        }

        public async Task<TokenResponseDiscordOAuth?> GetTokenAsync(string authorizationCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://discord.com/api/oauth2/token");

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = authorizationCode,
                ["client_id"] = oauthConfig.Value.AppId,
                ["client_secret"] = oauthConfig.Value.AppSecret,
                ["redirect_uri"] = navigationManager.BaseUri + "OAuthRedirectSuccessDiscord",
                ["grant_type"] = "authorization_code"
            });

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponseDiscordOAuth>(responseContent);
                return tokenResponse;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<UserInfoDiscordAPI?> GetUserInfoAsync(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://discord.com/api/users/@me");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserInfoDiscordAPI>(responseContent);
                return userInfo;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }
    }
}

using Google.Apis.Auth.OAuth2.Responses;
using IcantHumor.Models;
using IcantHumor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace IcantHumor.Services
{
    public class GoogleAPIService : IGoogleAPIService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;
        public GoogleAPIService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async Task<TokenResponseGoogleOAuth> GetTokenAsync(string authorizationCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token");

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = authorizationCode,
                ["client_id"] = "112990426089-bd2nau1l3dlgdjnl87q1tc8q4g8035sp.apps.googleusercontent.com",
                ["client_secret"] = "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",
                ["redirect_uri"] = navigationManager.BaseUri + "OAuthRedirectSuccessGoogle/",
                ["grant_type"] = "authorization_code"
            });

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponseGoogleOAuth>(responseContent);
                return tokenResponse;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"http status:{response.StatusCode}, message:{message}");
            }
        }

        public async Task<UserInfoGoogleAPI> GetUserInfoAsync(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v2/userinfo");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserInfoGoogleAPI>(responseContent);
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

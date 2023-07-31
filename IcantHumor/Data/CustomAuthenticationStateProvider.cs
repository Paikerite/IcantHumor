using Blazored.LocalStorage;
using IcantHumor.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace IcantHumor.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        public CustomAuthenticationStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            //var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            ////var identity = new ClaimsIdentity();

            //var user = new ClaimsPrincipal(identity);
            //var state = new AuthenticationState(user);

            //return state;
            string JwtToken = await _localStorage.GetItemAsStringAsync("token");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(JwtToken))
            {
                JwtToken = JwtToken.Replace("\"", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken securityToken;
                if (tokenHandler.CanReadToken(JwtToken))
                {
                    securityToken = tokenHandler.ReadToken(JwtToken) as JwtSecurityToken;
                }
                else
                {
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }

                //IEnumerable<Claim> claims = ParseClaimsFromJwt(JwtToken);
                //identity = new ClaimsIdentity(new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, claims.FirstOrDefault(key => key.Type == "unique_name").Value),
                //    new Claim(ClaimTypes.Role, claims.FirstOrDefault(key => key.Type == "role").Value),
                //}, "jwt");
                List<Claim> claims = securityToken.Claims.ToList();

                claims.AddRange(new List<Claim>{
                        new Claim(ClaimTypes.Name, claims.FirstOrDefault(key => key.Type == "unique_name").Value),
                        new Claim(ClaimTypes.Role, claims.FirstOrDefault(key => key.Type == "role").Value)
                });

                identity = new ClaimsIdentity(claims, "jwt");
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", JwtToken);
            }

            var state = new AuthenticationState(new ClaimsPrincipal(identity));

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        //public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        //{
        //    var payload = jwt.Split('.')[1];
        //    var jsonBytes = ParseBase64WithoutPadding(payload);
        //    var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        //    return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        //}

        //private static byte[] ParseBase64WithoutPadding(string base64)
        //{
        //    switch (base64.Length % 4)
        //    {
        //        case 2: base64 += "=="; break;
        //        case 3: base64 += "="; break;
        //    }
        //    return Convert.FromBase64String(base64);
        //}
    }
}

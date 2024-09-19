using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace IcantHumor.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly HttpClient _http;
        public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var JwtTokenResult = await _localStorage.GetAsync<string>("ICANTHUMOR512");

            string? JwtToken = JwtTokenResult.Success ? JwtTokenResult.Value : default;

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(JwtToken))
            {
                //JwtToken = JwtToken.Replace("\"", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken? securityToken;
                if (tokenHandler.CanReadToken(JwtToken))
                {
                    if (GenerationTokenJWT.ValidateCurrentToken(JwtToken))
                    { 
                        securityToken = tokenHandler.ReadToken(JwtToken) as JwtSecurityToken;
                    }
                    else
                    {
                        await _localStorage.DeleteAsync("ICANTHUMOR512");
                        return new AuthenticationState(new ClaimsPrincipal(identity));
                    }
                }
                else
                {
                    await _localStorage.DeleteAsync("ICANTHUMOR512");
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }

                List<Claim> claims = securityToken?.Claims.ToList() ?? throw new NullReferenceException("securityToken is null after reading token as jwtsecurityTOken");

                claims.AddRange(new List<Claim>{
                        new Claim(ClaimTypes.Name, claims.FirstOrDefault(key => key.Type == "unique_name")?.Value ?? throw new NullReferenceException("unique_name is null")),
                        new Claim(ClaimTypes.Role, claims.FirstOrDefault(key => key.Type == "role")?.Value ?? throw new NullReferenceException("role is null"))
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

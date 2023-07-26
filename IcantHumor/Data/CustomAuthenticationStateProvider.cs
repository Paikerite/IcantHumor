using IcantHumor.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace IcantHumor.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpContext httpContext;
        private ClaimsPrincipal claimsPrincipal = new(new ClaimsIdentity());

        private readonly ILogger _logger;
        public CustomAuthenticationStateProvider(HttpContext httpContext, ILogger logger)
        {
            this.httpContext = httpContext;
            _logger = logger;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                return Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
        }

        public async Task UpdateAuthenticationState(UserViewModel userSession)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userSession.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userSession.Role.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task Logout()
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

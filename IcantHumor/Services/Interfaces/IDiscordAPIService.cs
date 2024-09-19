using IcantHumor.Models;

namespace IcantHumor.Services.Interfaces
{
    public interface IDiscordAPIService
    {
        Task<TokenResponseDiscordOAuth?> GetTokenAsync(string authorizationCode);
        Task<UserInfoDiscordAPI?> GetUserInfoAsync(string accessToken);
    }
}

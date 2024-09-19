using IcantHumor.Models;

namespace IcantHumor.Services.Interfaces
{
    public interface IGoogleAPIService
    {
        Task<TokenResponseGoogleOAuth?> GetTokenAsync(string authorizationCode);
        Task<UserInfoGoogleAPI?> GetUserInfoAsync(string accessToken);
    }
}

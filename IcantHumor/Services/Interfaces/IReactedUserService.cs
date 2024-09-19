using IcantHumor.Models;
using Microsoft.AspNetCore.Mvc;

namespace IcantHumor.Services.Interfaces
{
    public interface IReactedUserService
    {
        Task<IEnumerable<ReactedUserViewModel>?> GetReactedUsers();
        Task<ReactedUserViewModel?> GetReactedUser(Guid id);
        Task<ReactedUserViewModel?> PutReactedUser(Guid id, ReactedUserViewModel reactedUserViewModel);
        Task<ReactedUserViewModel?> PostReactedUser(ReactedUserViewModel reactedUserViewModel);
        Task<ReactedUserViewModel?> DeleteReactedUser(Guid id);
    }
}

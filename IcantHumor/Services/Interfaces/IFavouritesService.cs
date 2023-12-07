using IcantHumor.Models;
using Microsoft.AspNetCore.Mvc;

namespace IcantHumor.Services.Interfaces
{
    public interface IFavouritesService
    {
        Task<IEnumerable<FavouriteViewModel>> GetFavourites();
        Task<FavouriteViewModel> GetFavourite(Guid id);
        Task<FavouriteViewModel> PutFavourite(Guid id, FavouriteViewModel favouriteViewModel);
        Task<FavouriteViewModel> PostFavourite(FavouriteViewModel favouriteViewModel);
        Task<FavouriteViewModel> DeleteFavourite(Guid id);
    }
}

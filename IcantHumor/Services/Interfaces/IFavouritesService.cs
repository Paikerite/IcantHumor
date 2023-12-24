using IcantHumor.Models;
using Microsoft.AspNetCore.Mvc;

namespace IcantHumor.Services.Interfaces
{
    public interface IFavouritesService
    {
        Task<IEnumerable<FavouriteViewModel>> GetFavourites();
        Task<FavouriteViewModel> GetFavourite(Guid id);
        Task<IEnumerable<FavouriteViewModel>> GetFavouritesByUser(Guid idUser);
        Task<IEnumerable<FavouriteViewModel>> GetCountFavouritesByUser(Guid idUser, int countFav);
        Task<FavouriteViewModel> PutFavourite(Guid id, FavouriteViewModel favouriteViewModel);
        Task<FavouriteViewModel> PostFavourite(FavouriteViewModel favouriteViewModel);
        Task<FavouriteViewModel> DeleteFavourite(Guid id);
    }
}

using IcantHumor.Models;
using Microsoft.AspNetCore.Mvc;

namespace IcantHumor.Services.Interfaces
{
    public interface IMediaFilesService
    {
        Task<IEnumerable<MediaViewModel>> GetMediaFiles();
        Task<MediaViewModel> GetMediaViewModel(Guid id);
        Task<MediaViewModel> PutMediaViewModel(Guid id, MediaViewModel mediaViewModel);
        Task<MediaViewModel> PostMediaViewModel(MediaViewModel mediaViewModel);
        Task<MediaViewModel> DeleteMediaViewModel(Guid id);
        Task<IEnumerable<MediaViewModel>> GetMediaFilesByCategory(Guid categoryId);
        Task<MediaViewModel> MakeReactionInPost(Guid idPost, ReactedUserViewModel reactedUser);
        Task<MediaViewModel> UnMakeReactionInPost(Guid idPost, ReactedUserViewModel reactedUser);
        Task<MediaViewModel> PatchCategoryInPost(Guid idPost, IEnumerable<Guid> categoriesIds);
    }
}

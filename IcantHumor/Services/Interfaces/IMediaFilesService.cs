using IcantHumor.Models;
using Microsoft.AspNetCore.Mvc;

namespace IcantHumor.Services.Interfaces
{
    public interface IMediaFilesService
    {
        Task<IEnumerable<MediaViewModel>> GetMediaFiles();
        Task<IEnumerable<MediaViewModel>> GetMediaFilesByCategories(IEnumerable<Guid> categories);
        Task<IEnumerable<MediaViewModel>> GetMediaFilesByName(string SearchText);
        Task<IEnumerable<MediaViewModel>> GetMediaFilesByNameByPages(string SearchText, int page, int itemsPerPage);
        Task<IEnumerable<MediaViewModel>> GetMediaPerPage(int page, int itemsPerPage);
        Task<IEnumerable<MediaViewModel>> GetCategorizedMediaPerPage(int page, int itemsPerPage, IEnumerable<Guid> categoriesIds);
        Task<int> GetCountMediaFiles();
        Task<int> GetCountMediaFilesIncludeCategories(IEnumerable<Guid> categoriesIds);
        Task<int> GetCountMediaFilesBySearch(string SearchText);
        Task<MediaViewModel> GetMediaViewModel(Guid id);
        Task<MediaViewModel> PutMediaViewModel(Guid id, MediaViewModel mediaViewModel);
        Task<MediaViewModel> PostMediaViewModel(MediaViewModel mediaViewModel);
        Task<MediaViewModel> DeleteMediaViewModel(Guid id);
        Task<IEnumerable<MediaViewModel>> GetMediaFilesByCategory(Guid categoryId);
        Task<MediaViewModel> MakeReactionInPost(Guid idPost, ReactedUserViewModel reactedUser);
        Task<MediaViewModel> UpdateTitlePost(Guid idPost, string title);
        Task<MediaViewModel> UnMakeReactionInPost(Guid idPost, ReactedUserViewModel reactedUser);
        Task<MediaViewModel> PatchCategoryInPost(Guid idPost, IEnumerable<Guid> categoriesIds);
    }
}

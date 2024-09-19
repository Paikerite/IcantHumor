using IcantHumor.Models;
using Microsoft.AspNetCore.Mvc;

namespace IcantHumor.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryViewModel>?> GetCategories();
        Task<CategoryViewModel?> GetCategory(Guid id);
        Task<CategoryViewModel?> GetCategoryByName(string name);
        Task<IEnumerable<CategoryViewModel>?> GetCategoriesByName(string SearchText);
        Task<CategoryViewModel?> PutCategory(Guid id, CategoryViewModel categoryViewModel);
        Task<CategoryViewModel?> PostCategory(CategoryViewModel categoryViewModel);
        Task<CategoryViewModel?> DeleteCategory(Guid id);
    }
}

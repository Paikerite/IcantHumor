using IcantHumor.Models;

namespace IcantHumor.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryViewModel>> GetCategories();
        Task<CategoryViewModel> GetCategory(Guid id);
        Task<CategoryViewModel> PutCategory(Guid id, CategoryViewModel categoryViewModel);
        Task<CategoryViewModel> PostCategory(CategoryViewModel categoryViewModel);
        Task<CategoryViewModel> DeleteCategory(Guid id);
    }
}

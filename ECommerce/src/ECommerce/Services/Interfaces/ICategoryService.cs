using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface ICategoryService
    {
        Category GetCategoryByTitle(string title);
        string GetParentCategoryTitle(string childCategoryTitle);
        void CreateCategory(string title, string parentCategories = null);
        bool CategoryExists(string title);
    }
}

using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
    public interface ICategoryService
    {
        Category GetCategoryByTitle(string title);
        string GetParentCategoryTitle(string childCategoryTitle);
        void CreateCategory(string title, string parentCategories = null);
        bool CategoryExists(string title);
    }
}

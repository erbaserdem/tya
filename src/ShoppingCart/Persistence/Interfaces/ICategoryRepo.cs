using ShoppingCart.Models;

namespace ShoppingCart.Persistence.Interfaces
{
    public interface ICategoryRepo
    {
        Category GetCategoryByTitle(string title);
        void CreateCategory(Category category);
    }
}

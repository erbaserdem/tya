using ECommerce.Models;

namespace ECommerce.Persistence.Interfaces
{
    public interface ICategoryRepo
    {
        Category GetCategoryByTitle(string title);
        void CreateCategory(Category category);
    }
}

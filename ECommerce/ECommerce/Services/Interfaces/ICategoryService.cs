using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface ICategoryService
    {
        Category GetCategoryByTitle(string title);
        void CreateCategory(string title, IEnumerable<string> parentCategories = null);
        bool CategoryTitleExists(string title);
    }
}

using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Models;
using ShoppingCart.Persistence.Interfaces;

namespace ShoppingCart.Persistence
{
    public class InMemoryCategoryRepo : List<Category>, ICategoryRepo
    {
        public Category GetCategoryByTitle(string title)
        {
            return this.FirstOrDefault(p => p.Title == title);
        }

        public void CreateCategory(Category category)
        {
            this.Add(category);
        }
    }
}
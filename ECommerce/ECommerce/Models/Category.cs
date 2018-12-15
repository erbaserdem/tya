using System.Collections.Generic;

namespace ECommerce.Models
{
    public class Category
    {
        public Category(string title, IEnumerable<string> parentCategories)
        {
            Title = title;
            ParentCategories = parentCategories;
        }

        public string Title { get; private set; }
        public IEnumerable<string> ParentCategories { get; private set; }
    }
}

namespace ECommerce.Models
{
    public class Category
    {
        public Category(string title, string parentCategory)
        {
            Title = title;
            ParentCategory = parentCategory;
        }

        public string Title { get; private set; }
        public string ParentCategory { get; private set; }
    }
}

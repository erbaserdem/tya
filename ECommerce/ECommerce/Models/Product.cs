namespace ECommerce.Models
{

    public class Product
    {
        public Product(decimal price, string title, string categoryTitle)
        {
            Price = price;
            Title = title;
            CategoryTitle = categoryTitle;
        }
        
        public decimal Price { get; }
        public string Title { get; }
        public string CategoryTitle { get; }
    }
}

namespace ECommerce.Models
{

    public class Product
    {
        public Product(double price, string title, string categoryTitle)
        {
            Price = price;
            Title = title;
            CategoryTitle = categoryTitle;
        }
        
        public double Price { get; }
        public string Title { get; }
        public string CategoryTitle { get; }
    }
}

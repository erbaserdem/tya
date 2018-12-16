using System.Collections.Generic;
using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
    public interface IProductService
    {
        Product GetProductByTitle(string title);
        IEnumerable<Product> GetProductsByCategoryTitle(string title);
        void CreateProduct(double price, string title, string categoryTitle);
        bool ProductExists(string productTitle);
    }
}

using ECommerce.Models;
using System.Collections.Generic;

namespace ECommerce.Services
{
    public interface IProductService
    {
        Product GetProductByTitle(string title);
        IEnumerable<Product> GetProductsByCategoryTitle(string title);
        void CreateProduct(decimal price, string title, string categoryTitle);
        bool ProductExists(string productTitle);
    }
}

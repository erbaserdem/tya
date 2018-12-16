using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IProductService
    {
        Product GetProductByTitle(string title);
        IEnumerable<Product> GetProductsByCategoryTitle(string title);
        void CreateProduct(decimal price, string title, string categoryTitle);
        bool ProductExists(string productTitle);
    }
}

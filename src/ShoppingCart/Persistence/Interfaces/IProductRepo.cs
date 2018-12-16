using System.Collections.Generic;
using ShoppingCart.Models;

namespace ShoppingCart.Persistence.Interfaces
{
    public interface IProductRepo
    {
        Product GetProductByTitle(string title);
        IEnumerable<Product> GetProductsByCategory(string title);
        void CreateProduct(Product product);
    }
}

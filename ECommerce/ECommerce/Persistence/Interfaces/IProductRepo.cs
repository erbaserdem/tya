using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Persistence.Interfaces
{
    public interface IProductRepo
    {
        Product GetProductByTitle(string title);
        IEnumerable<Product> GetProductsByCategory(string title);
        void CreateProduct(Product product);
    }
}

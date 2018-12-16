using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Models;
using ShoppingCart.Persistence.Interfaces;

namespace ShoppingCart.Persistence
{
    public class InMemoryProductRepo : List<Product>, IProductRepo
    {
        public Product GetProductByTitle(string title)
        {
            return this.FirstOrDefault(p => p.Title == title);
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryTitle)
        {
            return this.Where(p => p.CategoryTitle== categoryTitle);
        }

        public void CreateProduct(Product product)
        {
            this.Add(product);
        }
    }
}
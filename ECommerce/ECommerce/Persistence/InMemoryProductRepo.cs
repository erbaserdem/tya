using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Persistence.Interfaces;

namespace ECommerce.Persistence
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
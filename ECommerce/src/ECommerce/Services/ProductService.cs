using System;
using System.Collections.Generic;
using ECommerce.Models;
using ECommerce.Persistence.Interfaces;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services
{
    public class ProductService : IProductService
    {
        private IProductRepo ProductRepo;
        private ICategoryService CategoryService;

        public ProductService(IProductRepo productRepo, ICategoryService categoryService)
        {
            ProductRepo = productRepo;
            CategoryService = categoryService;
        }

        public Product GetProductByTitle(string title)
        {
            return ProductRepo.GetProductByTitle(title);
        }

        public IEnumerable<Product> GetProductsByCategoryTitle(string title)
        {
            return ProductRepo.GetProductsByCategory(title);
        }

        public void CreateProduct(decimal price, string title, string categoryTitle)
        {
            if (!CategoryService.CategoryExists(categoryTitle))
            {
                throw new Exception($"Category with title: {categoryTitle} does not exists");
            }
            if (price<=0)
            {
                throw new Exception($"Price: {price} should be positive");
            }
            if (ProductExists(title))
            {
                throw new Exception($"Product with title: {title} already exists");
            }
            ProductRepo.CreateProduct(new Product(price, title, categoryTitle));
        }

        public bool ProductExists(string productTitle)
        {
            return ProductRepo.GetProductByTitle(productTitle) != null;
        }
    }
}
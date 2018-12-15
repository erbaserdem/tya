using System;
using System.Collections.Generic;
using ECommerce.Models;
using ECommerce.Persistence.Interfaces;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services
{
    public class CategoryService : ICategoryService
    {
        public ICategoryRepo CategoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            CategoryRepo = categoryRepo;
        }

        public Category GetCategoryByTitle(string title)
        {
            return CategoryRepo.GetCategoryByTitle(title);
        }

        public void CreateCategory(string title, IEnumerable<string> parentCategories = null)
        {
            foreach (var parentCategory in parentCategories ?? new List<string>())
            {
                if (!CategoryExists(parentCategory))
                {
                    throw new Exception($"Category with title: {parentCategory} does not exists");
                }
            }
            if (CategoryExists(title))
            {
                throw new Exception($"Category with title: {title} already exists");
            }
            CategoryRepo.CreateCategory(new Category(title, parentCategories));
        }

        public bool CategoryExists(string title)
        {
            return CategoryRepo.GetCategoryByTitle(title) != null;
        }
    }
}
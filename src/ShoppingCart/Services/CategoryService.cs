﻿using System;
using ShoppingCart.Models;
using ShoppingCart.Persistence.Interfaces;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.Services
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

        public string GetParentCategoryTitle(string childCategoryTitle)
        {
            return CategoryRepo.GetCategoryByTitle(childCategoryTitle).ParentCategory;
        }

        public void CreateCategory(string title, string parentCategory = null)
        {
            if (parentCategory != null && !CategoryExists(parentCategory))
            {
                throw new Exception($"Category with title: {parentCategory} does not exists");
            }
            if (CategoryExists(title))
            {
                throw new Exception($"Category with title: {title} already exists");
            }
            CategoryRepo.CreateCategory(new Category(title, parentCategory));
        }

        public bool CategoryExists(string title)
        {
            return CategoryRepo.GetCategoryByTitle(title) != null;
        }
    }
}
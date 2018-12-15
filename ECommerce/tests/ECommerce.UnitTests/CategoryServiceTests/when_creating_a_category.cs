using System.Collections.Generic;
using ECommerce.Persistence;
using ECommerce.Services;
using FluentAssertions;
using NUnit.Framework;

namespace ECommerce.UnitTests.CategoryServiceTests
{
    public class when_creating_a_category
    {
        private IEnumerable<string> parentCategories = new List<string>{"parent1", "parent2"};
        private string categoryTitle = "CategoryTitle";
        private CategoryService categoryService;
        private InMemoryCategoryRepo categoryRepo;
        

        [OneTimeSetUp]
        public void Setup()
        {
            categoryRepo = new InMemoryCategoryRepo();
            SetUpParentCategories();
            categoryService = new CategoryService(categoryRepo);
            categoryService.CreateCategory(categoryTitle, parentCategories);
        }

        [Test]
        public void it_should_create_campaign_with_given_parameters()
        {
            var category = categoryService.GetCategoryByTitle(categoryTitle);
            category.Title.Should().Be(categoryTitle);
            foreach (var parentCategory in parentCategories)
            {
                category.ParentCategories.Should().Contain(parentCategory);
            }
        }

        private void SetUpParentCategories()
        {
            foreach (var parentCategory in parentCategories)
            {
                categoryRepo.Add(new Models.Category(parentCategory, new List<string>()));
            }
        }
    }
}
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Models;
using ShoppingCart.Persistence;
using ShoppingCart.Services;

namespace ShoppingCart.UnitTests.CategoryServiceTests
{
    public class when_creating_a_category
    {
        private string parentCategory = "parent";
        private string categoryTitle = "CategoryTitle";
        private CategoryService categoryService;
        private InMemoryCategoryRepo categoryRepo;


        [OneTimeSetUp]
        public void Setup()
        {
            categoryRepo = new InMemoryCategoryRepo();
            SetUpParentCategories();
            categoryService = new CategoryService(categoryRepo);
            categoryService.CreateCategory(categoryTitle, parentCategory);
        }

        [Test]
        public void it_should_create_campaign_with_given_parameters()
        {
            var category = categoryService.GetCategoryByTitle(categoryTitle);
            category.Title.Should().Be(categoryTitle);
            category.ParentCategory.Should().Contain(parentCategory);
        }

        private void SetUpParentCategories()
        {
            categoryRepo.Add(new Category(parentCategory, null));
        }
    }
}
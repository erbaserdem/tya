using System;
using ECommerce.Persistence;
using ECommerce.Services;
using FluentAssertions;
using NUnit.Framework;

namespace ECommerce.UnitTests.CategoryServiceTests
{
    public class when_creating_a_category_with_nonexisting_parent_categories
    {
        private string parentCategory = "parent3";
        private string categoryTitle = "CategoryTitle";
        private CategoryService categoryService;
        private InMemoryCategoryRepo categoryRepo;
        private Action action;
        

        [OneTimeSetUp]
        public void Setup()
        {
            categoryRepo = new InMemoryCategoryRepo();
            categoryService = new CategoryService(categoryRepo);
            action = () =>
            {
                categoryService.CreateCategory(categoryTitle, parentCategory);
            };
        }

        [Test]
        public void it_should_throw_exception_abou_parent_category_existency()
        {
            action.Should().Throw<Exception>();
        }
    }
}
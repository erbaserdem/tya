using System;
using ECommerce.Persistence;
using ECommerce.Services;
using ECommerce.Services.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ECommerce.UnitTests.ProductServiceTests
{
    public class when_creating_a_product_with_nonexisting_category
    {
        private double price = 35;
        private string productTitle = "ProductTitle";
        private string categoryTitle = "CategoryTitle";
        private ProductService productService;
        private InMemoryProductRepo productRepo;
        private Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
        private Action action;
        

        [OneTimeSetUp]
        public void Setup()
        {
            productRepo = new InMemoryProductRepo();
            categoryService.Setup(c=>c.CategoryExists(It.IsAny<string>())).Returns(false);
            productService = new ProductService(productRepo, categoryService.Object);
            action = () => { productService.CreateProduct(price, productTitle, categoryTitle); };

        }

        [Test]
        public void it_should_throw_exception_about_category_existency()
        {
            action.Should().Throw<Exception>();
        }
    }
}
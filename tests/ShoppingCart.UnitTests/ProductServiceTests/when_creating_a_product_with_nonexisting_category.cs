using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ShoppingCart.Persistence;
using ShoppingCart.Services;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.UnitTests.ProductServiceTests
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
        public void it_should_not_create_product_due_to_category_being_nonexistent()
        {
            action.Should().Throw<Exception>();
        }
    }
}
using ECommerce.Persistence;
using ECommerce.Services;
using ECommerce.Services.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ECommerce.UnitTests.ProductServiceTests
{
    public class when_creating_a_product
    {
        private decimal price = 35m;
        private string productTitle = "ProductTitle";
        private string categoryTitle = "CategoryTitle";
        private ProductService productService;
        private InMemoryProductRepo productRepo;
        private Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
        

        [OneTimeSetUp]
        public void Setup()
        {
            productRepo = new InMemoryProductRepo();
            categoryService.Setup(c=>c.CategoryExists(It.IsAny<string>())).Returns(true);
            productService = new ProductService(productRepo, categoryService.Object);
            productService.CreateProduct(price, productTitle, categoryTitle);
        }

        [Test]
        public void it_should_create_product_with_given_parameters()
        {
            var product = productService.GetProductByTitle(productTitle);
            product.CategoryTitle.Should().Be(categoryTitle);
            product.Title.Should().Be(productTitle);
            product.Price.Should().Be(price);
        }
    }
}
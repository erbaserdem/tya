using System.Linq;
using ECommerce.Models;
using ECommerce.Persistence;
using ECommerce.Services;
using ECommerce.Services.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ECommerce.UnitTests.ShoppingCartServiceTests
{
    public class when_adding_an_item_to_cart : TestBase
    {
        [OneTimeSetUp]
        public override void Setup()
        {
            base.Setup();
            cartService.AddItemToCart(cart, productToAddCart.Title, quantity);
        }


        [Test]
        public void it_should_add_given_product_to_cart_with_given_quantity()
        {
            var cartItem = cart.Items.First();
            cartItem.Product.Title.Should().Be(productToAddCart.Title);
            cartItem.TotalItemAmount.Should().Be(productToAddCart.Price * quantity);
        }


        protected override void SetUpData()
        {
            productToAddCart = new Product(50m, "productTitle", "categoryTitle");
        }

        protected override void SetUpMocks()
        {
            productService.Setup(p => p.GetProductByTitle(productToAddCart.Title)).Returns(productToAddCart);
        }

    }
}
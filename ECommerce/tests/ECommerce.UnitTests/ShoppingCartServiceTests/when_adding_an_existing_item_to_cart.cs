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
    public class when_adding_an_existing_item_to_cart : TestBase
    {
        [OneTimeSetUp]
        public override void Setup()
        {
            base.Setup();
            cartService.AddItemToCart(cart, productToAddCart.Title, Quantity);
            cartService.AddItemToCart(cart, productToAddCart.Title, Quantity);
        }


        [Test]
        public void it_should_add_given_product_to_cart_with_given_quantity_on_to_same_item()
        {
            var cartItem = cart.Items.First();
            cartItem.Product.Title.Should().Be(productToAddCart.Title);
            cartItem.TotalItemAmount.Should().Be(productToAddCart.Price * Quantity * 2);
        }
    }
}
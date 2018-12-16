using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ShoppingCart.UnitTests.ShoppingCartServiceTests
{
    public class when_adding_an_item_to_cart : TestBase
    {
        [OneTimeSetUp]
        public override void Setup()
        {
            base.Setup();
            cartService.AddItemToCart(productToAddCart.Title, Quantity);
        }


        [Test]
        public void it_should_add_given_product_to_cart_with_given_quantity()
        {
            var cartItem = cartService.GetCart().Items.First();
            cartItem.Product.Title.Should().Be(productToAddCart.Title);
            cartItem.TotalItemAmount.Should().Be(productToAddCart.Price * Quantity);
        }

    }
}
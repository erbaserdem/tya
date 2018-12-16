using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ShoppingCart.UnitTests.ShoppingCartServiceTests
{
    public class when_applying_campaigns_to_cart_with_best_discount_being_rate_type : TestBase
    {
        [OneTimeSetUp]
        public override  void Setup()
        {
            amountTypeDiscountAmount = 1;
            rateTypeDiscountAmount = 10;
            base.Setup();
            cartService.AddItemToCart(productToAddCart.Title, Quantity);
            cartService.ApplyOrUpdateCampaignsToCart();
        }


        [Test]
        public void it_should_apply_best_possible_campaign_to_cart_item()
        {
            var cart = cartService.GetCart();
            var cartItem = cartService.GetCart().Items.First();
            cartItem.Product.Title.Should().Be(productToAddCart.Title);
            cartItem.TotalItemAmount.Should().Be(productToAddCart.Price * Quantity);

            cart.ItemsTotalDiscountedAmount.Should().BeLessThan(cart.ItemsTotalAmount);
            cart.ItemsTotalDiscountedAmount.Should().Be(cart.ItemsTotalAmount * (100 - rateTypeDiscountAmount) / 100);

        }
    }
}
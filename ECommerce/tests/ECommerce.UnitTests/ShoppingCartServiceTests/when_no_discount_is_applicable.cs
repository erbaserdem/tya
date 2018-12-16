using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ECommerce.UnitTests.ShoppingCartServiceTests
{
    public class when_no_campaign_is_applicable : TestBase
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
        public void it_should_not_change_carts_discounted_amount_if_no_campaign_is_applied()
        {
            var cart = cartService.GetCart();
            var cartItem = cart.Items.First();
            cartItem.Product.Title.Should().Be(productToAddCart.Title);
            cartItem.TotalItemAmount.Should().Be(productToAddCart.Price * Quantity);

            cart.ItemsTotalDiscountedAmount.Should().Be(cart.ItemsTotalAmount);

        }

        protected override void SetUpData()
        {
            AmountTypedDiscountCategory = ParentCategory;
            RateTypedDiscountCategory = ParentCategory;
            ProductCategory = NoDiscount;
            base.SetUpData();
        }
    }
}
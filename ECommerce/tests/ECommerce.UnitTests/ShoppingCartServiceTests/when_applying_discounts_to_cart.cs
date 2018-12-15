using System.Collections.Generic;
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
    public class when_applying_discounts_to_cart : TestBase
    {
        [OneTimeSetUp]
        public override  void Setup()
        {
            base.Setup();
            cartService.AddItemToCart(cart, productToAddCart.Title, quantity);
            cartService.ApplyDiscountsToCart(cart);
        }


        [Test]
        public void it_should_apply_bes_possible_campaign_to_cart_item()
        {
            var cartItem = cart.Items.First();
            cartItem.Product.Title.Should().Be(productToAddCart.Title);
            cartItem.TotalItemAmount.Should().Be(productToAddCart.Price * quantity);

            cart.ItemsTotalDiscountedAmount.Should().BeLessThan(cart.ItemsTotalAmount);
            cart.ItemsTotalDiscountedAmount.Should().Be(cart.ItemsTotalAmount *85 /100);

        }

        protected override void SetUpData()
        {
            productToAddCart = new Product(50m, "productTitle", "categoryTitle");
            campaigns = new List<Campaign>
            {
                new Campaign(productToAddCart.CategoryTitle, 15, quantity-10, DiscountType.Amount),
                new Campaign(productToAddCart.CategoryTitle, 15, quantity-10, DiscountType.Rate)
            };
        }

        protected override void SetUpMocks()
        {
            productService.Setup(p => p.GetProductByTitle(productToAddCart.Title)).Returns(productToAddCart);
            campaignService.Setup(a =>
                a.GetCampaignsByCategoryTitleAndMinimumQuantity(productToAddCart.CategoryTitle, quantity)).Returns(campaigns);
        }

    }
}
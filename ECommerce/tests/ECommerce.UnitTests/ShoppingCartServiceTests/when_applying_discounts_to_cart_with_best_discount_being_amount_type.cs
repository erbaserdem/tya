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
    public class when_applying_discounts_to_cart_with_best_discount_being_amount_type : TestBase
    {
        [OneTimeSetUp]
        public override  void Setup()
        {
            rateTypeDiscountAmount = 1;
            amountTypeDiscountAmount = 40;
            base.Setup();
            cartService.AddItemToCart(productToAddCart.Title, Quantity);
            cartService.ApplyOrUpdateCampaignsToCart();
        }


        [Test]
        public void it_should_apply_bes_possible_campaign_to_cart_item()
        {

            var cart = cartService.GetCart();
            var cartItem = cart.Items.First();
            cartItem.Product.Title.Should().Be(productToAddCart.Title);
            cartItem.TotalItemAmount.Should().Be(productToAddCart.Price * Quantity);

            cart.ItemsTotalDiscountedAmount.Should().BeLessThan(cart.ItemsTotalAmount);
            cart.ItemsTotalDiscountedAmount.Should().Be(cart.ItemsTotalAmount - Quantity* amountTypeDiscountAmount);

        }

    }
}
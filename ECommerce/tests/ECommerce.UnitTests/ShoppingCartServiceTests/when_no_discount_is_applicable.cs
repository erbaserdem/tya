using System;
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
    public class when_no_discount_is_applicable : TestBase
    {
        [OneTimeSetUp]
        public override  void Setup()
        {
            amountTypeDiscountAmount = 1;
            rateTypeDiscountAmount = 10;
            base.Setup();
            cartService.AddItemToCart(cart, productToAddCart.Title, Quantity);
            cartService.ApplyOrUpdateCampaignsToCart(cart);
        }


        [Test]
        public void it_should_apply_parent_categorys_discount_if_none_defined_for_its_own()
        {
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
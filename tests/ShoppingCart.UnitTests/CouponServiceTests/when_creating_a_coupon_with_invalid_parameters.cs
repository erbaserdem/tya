using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.UnitTests.CouponServiceTests
{
    class when_creating_a_coupon_with_invalid_parameters
    {
        private ICouponService CouponService = new CouponService();
        private Coupon coupon;
        private Action action;

        [TestCase(100, 100, DiscountType.Rate)]
        [TestCase(100, -5, DiscountType.Rate)]
        [TestCase(100, 100, DiscountType.Amount)]
        [TestCase(50, 100, DiscountType.Amount)]
        [TestCase(100, -50, DiscountType.Amount)]
        public void it_should_not_create_coupon_due_to_incompatible_amount_and_minimum_amount(double minAmount, double amount, DiscountType type)
        {
            action = () => { CouponService.CreateCoupon(minAmount, amount, type); };
            action.Should().Throw<Exception>();
        }
    }
}

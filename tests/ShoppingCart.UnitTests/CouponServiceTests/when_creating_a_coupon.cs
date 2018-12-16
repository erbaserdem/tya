using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.UnitTests.CouponServiceTests
{
    class when_creating_a_coupon
    {
        private ICouponService CouponService = new CouponService();
        private const double amount = 4, minAmount = 5;
        private const DiscountType type = DiscountType.Amount;
        private Coupon coupon;



        [OneTimeSetUp]
        public void SetUp()
        {
            coupon = CouponService.CreateCoupon(minAmount, amount, type);
        }

        [Test]
        public void it_should_create_coupon_with_given_parameters()
        {
            coupon.Amount.Should().Be(amount);
            coupon.MinCartAmount.Should().Be(minAmount);
            coupon.Type.Should().Be(type);
            coupon.Status.Should().Be(CouponStatus.Active);
        }
    }
}

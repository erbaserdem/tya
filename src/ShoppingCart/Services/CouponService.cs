using System;
using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.Services
{
    public class CouponService : ICouponService
    {
        public Coupon CreateCoupon(double minAmount, double amount, DiscountType type)
        {
            EnsureCouponIsValid(minAmount, amount, type);
            return new Coupon(minAmount, amount, type);
        }


        private void EnsureCouponIsValid( double minAmount, double amount, DiscountType type)
        {
            if (amount <= 0)
            {
                throw new Exception($"Discount amount can not be negatice regardless of type");
            }
            if (type == DiscountType.Rate && (amount >= 100 || amount <= 0))
            {
                throw new Exception($"Discount amount should be between 0 and 100 for rate typed discounts");
            }
            if (type == DiscountType.Amount && amount >= minAmount)
            {
                throw new Exception($"Coupon amount can not be greater than minimum amount");
            }
        }
    }
}
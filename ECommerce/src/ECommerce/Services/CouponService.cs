using System;
using ECommerce.Models;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services
{
    class CouponService : ICouponService
    {
        private IProductService ProductService;

        public CouponService(IProductService productService)
        {
            ProductService = productService;
        }

        public Coupon CreateCampaign(double minAmount, double amount, DiscountType type)
        {
            EnsureCouponIsValid(minAmount, amount, type);
            return new Coupon(minAmount, amount, type);
        }


        private void EnsureCouponIsValid( double minAmount, double amount, DiscountType type)
        {
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
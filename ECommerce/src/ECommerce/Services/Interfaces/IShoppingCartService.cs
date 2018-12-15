﻿using System.Collections.Generic;
using System.Text;
using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IShoppingCartService
    {
        void ApplyDiscountsToCart(ShoppingCart cart);

        void ApplyCouponToCart(ShoppingCart cart, Coupon coupon);

        void AddItemToCart(ShoppingCart cart, string productTitle, int quantity);
    }
}
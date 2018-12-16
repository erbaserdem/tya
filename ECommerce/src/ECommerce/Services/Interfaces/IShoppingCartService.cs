using System.Collections.Generic;
using System.Text;
using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IShoppingCartService
    {
        void ApplyOrUpdateCampaignsToCart(ShoppingCart cart);

        void ApplyCouponToCart(ShoppingCart cart, Coupon coupon);

        void AddItemToCart(ShoppingCart cart, string productTitle, int quantity);
        void SetOrUpdateDeliveryCost(ShoppingCart cart);
        string GetCartInfo(ShoppingCart cart);
    }
}

using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
    public interface IShoppingCartService
    {
        void ApplyOrUpdateCampaignsToCart();

        void ApplyCouponToCart(Coupon coupon);

        void AddItemToCart(string productTitle, int quantity);
        void SetOrUpdateDeliveryCost();
        string GetCartInfo();
    }
}

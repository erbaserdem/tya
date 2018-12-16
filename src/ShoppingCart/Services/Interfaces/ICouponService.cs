using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
    public interface ICouponService
    {
        Coupon CreateCoupon(double minAmount, double amount, DiscountType type);
    }
}

using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface ICouponService
    {
        Coupon CreateCoupon(double minAmount, double amount, DiscountType type);
    }
}

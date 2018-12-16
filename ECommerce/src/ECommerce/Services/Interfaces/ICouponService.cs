using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    interface ICouponService
    {
        Coupon CreateCampaign(double minAmount, double amount, DiscountType type);
    }
}

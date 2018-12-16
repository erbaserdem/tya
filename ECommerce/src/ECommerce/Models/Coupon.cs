namespace ECommerce.Models
{
    public class Coupon
    {
        public Coupon(double minCartAmount, double amount, DiscountType type)
        {
            MinCartAmount = minCartAmount;
            Amount = amount;
            Type = type;
            Status = CouponStatus.Active;
        }

        public void SetStatusInUse()
        {
            Status = CouponStatus.InUse;
        }


        public double MinCartAmount { get; private set; }
        public double Amount { get; private set; }
        public DiscountType Type{ get; private set; }
        public CouponStatus Status{ get; private set; }
    }

    public enum CouponStatus
    {
        Active,
        InUse,
        Used
    }
}

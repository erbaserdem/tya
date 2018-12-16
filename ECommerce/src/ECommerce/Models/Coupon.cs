namespace ECommerce.Models
{
    public class Coupon
    {
        public Coupon(decimal minCartAmount, decimal amount, DiscountType type)
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


        public decimal MinCartAmount { get; private set; }
        public decimal Amount { get; private set; }
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

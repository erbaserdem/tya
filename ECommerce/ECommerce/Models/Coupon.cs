namespace ECommerce.Models
{
    class Coupon
    {
        public Coupon(decimal minCartAmount, decimal amount, DiscountType type, CouponStatus status)
        {
            MinCartAmount = minCartAmount;
            Amount = amount;
            Type = type;
            Status = status;
        }

        private void SetStatus(CouponStatus status)
        {
            Status = status;
        }


        public decimal MinCartAmount { get; private set; }
        public decimal Amount { get; private set; }
        public DiscountType Type{ get; private set; }
        public CouponStatus Status{ get; private set; }
    }

    enum CouponStatus
    {
        Active,
        InUse,
        Used
    }
}

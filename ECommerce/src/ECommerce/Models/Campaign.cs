namespace ECommerce.Models
{
    public class Campaign
    {
        public Campaign(string categoryTitle, decimal amount, int minItemQty, DiscountType type)
        {
            CategoryTitle = categoryTitle;
            Amount = amount;
            MinItemQty = minItemQty;
            Type = type;
        }

        public string CategoryTitle { get; set; }
        public decimal Amount { get; set; }
        public int MinItemQty { get; set; }
        public DiscountType Type{ get; set; }
    }
}

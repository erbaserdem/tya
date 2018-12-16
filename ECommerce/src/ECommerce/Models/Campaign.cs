namespace ECommerce.Models
{
    public class Campaign
    {
        public Campaign(string categoryTitle, double amount, int minItemQty, DiscountType type)
        {
            CategoryTitle = categoryTitle;
            Amount = amount;
            MinItemQty = minItemQty;
            Type = type;
        }

        public string CategoryTitle { get; set; }
        public double Amount { get; set; }
        public int MinItemQty { get; set; }
        public DiscountType Type{ get; set; }
    }
}

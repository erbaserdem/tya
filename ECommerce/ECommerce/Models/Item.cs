namespace ECommerce.Models
{
    public class Item
    {
        public Item(Product product, int quantity, decimal totalItemAmount)
        {
            Product = product;
            Quantity = quantity;
            TotalItemAmount = totalItemAmount;
            TotalDiscountedItemAmount = totalItemAmount;
        }

        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalItemAmount { get; private set; }
        public decimal TotalDiscountedItemAmount { get; private set; }


        public void SetQuantity(int qty)
        {
            Quantity = qty;
        }
        public void SetTotalItemAmount(decimal amount)
        {
            TotalItemAmount = amount;
        }
        public void SetTotalDiscountedItemAmount(decimal discountedAmount)
        {
            TotalDiscountedItemAmount = discountedAmount;
        }
    }
}
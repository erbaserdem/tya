namespace ECommerce.Models
{
    public class Item
    {
        public Item(Product product, int quantity, double totalItemAmount)
        {
            Product = product;
            Quantity = quantity;
            TotalItemAmount = totalItemAmount;
            TotalDiscountedItemAmount = totalItemAmount;
        }

        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public double TotalItemAmount { get; private set; }
        public double TotalDiscountedItemAmount { get; private set; }


        public void SetQuantity(int qty)
        {
            Quantity = qty;
        }
        public void SetTotalItemAmount(double amount)
        {
            TotalItemAmount = amount;
        }
        public void SetTotalDiscountedItemAmount(double discountedAmount)
        {
            TotalDiscountedItemAmount = discountedAmount;
        }
    }
}
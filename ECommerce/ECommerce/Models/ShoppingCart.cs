using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Models
{
    class ShoppingCart
    {
        public ShoppingCart(IEnumerable<Item> items, decimal couponDiscountAmount)
        {
            Items = items;
            CouponDiscountAmount = couponDiscountAmount;
        }

        public ShoppingCart()
        {
            Items = new List<Item>();
            CouponDiscountAmount = 0;
        }

        public IEnumerable<Item> Items { get; private set; }

        public decimal ItemsTotalAmount
        {
            get
            {
                decimal c = 0;
                foreach (var item in Items)
                {
                    c += item.Quantity * item.Product.Price;
                }

                return c;
            }
        }

        public decimal ItemsTotalDiscountedAmount
        {
            get
            {
                decimal c = 0;
                foreach (var item in Items)
                {
                    c += item.Quantity * item.TotalItemAmount;
                }

                return c;
            }
        }
        public decimal CouponDiscountAmount { get; private set; }

        public void AddLineItem(Item item)
        {
            Items.Append(item);
        }

        private void SetCouponDiscountAmount(decimal couponDiscountAmount)
        {
            CouponDiscountAmount = couponDiscountAmount;
        }
    }

    class Item
    {
        public Item(Product product, int quantity, decimal totalItemAmount)
        {
            Product = product;
            Quantity = quantity;
            TotalItemAmount = totalItemAmount;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalItemAmount { get; private set; }
        public decimal TotalDiscountedItemAmount { get; private set; }

        public void SetTotalDiscountedItemAmount(decimal discountedAmount)
        {
            TotalDiscountedItemAmount = discountedAmount;
        }
    }
}

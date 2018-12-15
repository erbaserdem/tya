using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Models
{
    public class ShoppingCart
    {
        public ShoppingCart(List<Item> items, decimal couponDiscountAmount)
        {
            Items = items;
            CouponDiscountAmount = couponDiscountAmount;
        }

        public ShoppingCart()
        {
            Items = new List<Item>();
            CouponDiscountAmount = 0;
        }

        public List<Item> Items { get; private set; }

        public decimal ItemsTotalAmount
        {
            get
            {
                decimal c = 0;
                foreach (var item in Items)
                {
                    c +=item.TotalItemAmount;
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
                    c += item.TotalDiscountedItemAmount;
                }

                return c;
            }
        }
        public decimal CouponDiscountAmount { get; private set; }

        public void AddLineItem(Item item)
        {
            var existingItemWithSameProduct = Items.FirstOrDefault(i => i.Product.Title == item.Product.Title);
            if (existingItemWithSameProduct != null)
            {
                existingItemWithSameProduct.SetQuantity(item.Quantity + existingItemWithSameProduct.Quantity);
                existingItemWithSameProduct.SetTotalDiscountedItemAmount(item.TotalDiscountedItemAmount + existingItemWithSameProduct.TotalDiscountedItemAmount);
                existingItemWithSameProduct.SetTotalItemAmount(item.TotalItemAmount + existingItemWithSameProduct.TotalItemAmount);
            }
            else
            {
                Items.Add(item);
            }
        }

        public void SetCouponDiscountAmount(decimal couponDiscountAmount)
        {
            CouponDiscountAmount = couponDiscountAmount;
        }
    }
}

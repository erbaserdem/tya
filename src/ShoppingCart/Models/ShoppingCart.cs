using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<Item>();
        }

        public List<Item> Items { get; private set; }

        public double ItemsTotalAmount
        {
            get
            {
                double c = 0;
                foreach (var item in Items)
                {
                    c +=item.TotalItemAmount;
                }

                return c;
            }
        }

        public double ItemsTotalDiscountedAmount
        {
            get
            {
                double c = 0;
                foreach (var item in Items)
                {
                    c += item.TotalDiscountedItemAmount;
                }

                return c;
            }
        }

        public double CouponDiscountAmount { get; private set; }

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

        public void SetCouponDiscountAmount(double couponDiscountAmount)
        {
            CouponDiscountAmount = couponDiscountAmount;
        }

        public double DeliveryCost { get; private set; }

        public void SetDeliveryCost(double deliveryCost)
        {
            DeliveryCost = deliveryCost;
        }

        public double GetCouponDiscount()
        {
            return CouponDiscountAmount;
        }

        public double GetCampaignDiscount()
        {
            return ItemsTotalAmount-ItemsTotalDiscountedAmount;
        }

        public double GetTotalCartAmountAfterDiscounts()
        {
            return ItemsTotalDiscountedAmount - CouponDiscountAmount;
        }
        public double GetTotalCartAmount()
        {
            return ItemsTotalAmount;
        }

        public double GetDeliveryCost()
        {
            return DeliveryCost;
        }
    }
}

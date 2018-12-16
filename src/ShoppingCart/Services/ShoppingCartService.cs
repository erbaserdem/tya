using System;
using System.Linq;
using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private IProductService ProductService;
        private ICampaignService CampaignService;
        private IDeliveryCostCalculatorService DeliveryCostCalculatorService;
        private Models.ShoppingCart cart;

        public ShoppingCartService(IProductService productService, IDeliveryCostCalculatorService deliveryCostCalculatorService, ICampaignService campaignService)
        {
            ProductService = productService;
            DeliveryCostCalculatorService = deliveryCostCalculatorService;
            CampaignService = campaignService;
            cart = new Models.ShoppingCart();
        }

        public void ApplyOrUpdateCampaignsToCart()
        {
            foreach (var cartItem in cart.Items)
            {
                var categoryTitle = cartItem.Product.CategoryTitle;
                var discountAmount = GetBestPossibleDiscountAmountForItem(categoryTitle, cartItem.Quantity, cartItem.TotalItemAmount);
                cartItem.SetTotalDiscountedItemAmount(cartItem.TotalItemAmount - discountAmount);
            }
        }

        private double GetBestPossibleDiscountAmountForItem(string categoryTitle, int cartItemQuantity, double cartItemTotalItemAmount)
        {
            var campaigns =
                CampaignService.GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(categoryTitle, cartItemQuantity);
            if (campaigns == null) return 0;

            var rateTypeCampaigns = campaigns.Where(c => c.Type == DiscountType.Rate);
            var bestRateTypeCampaignAmount = rateTypeCampaigns.Any() ? rateTypeCampaigns.Max(c=>c.Amount) : 0;
            var amountTypeCampaigns = campaigns.Where(c => c.Type == DiscountType.Amount);
            var bestAmountTypeCampaignAmount = amountTypeCampaigns.Any() ? amountTypeCampaigns.Max(c => c.Amount) : 0;

            var rateTypeDiscountAmount = cartItemTotalItemAmount * bestRateTypeCampaignAmount / 100;
            var amountTypeDiscountAmount = bestAmountTypeCampaignAmount * cartItemQuantity;

            return rateTypeDiscountAmount > amountTypeDiscountAmount
                ? rateTypeDiscountAmount
                : amountTypeDiscountAmount;
        }

        public void ApplyCouponToCart(Coupon coupon)
        {
            if (coupon.Status != CouponStatus.Active)
            {
                throw new Exception("Coupon is already used or it is in use by this or another cart");
            }
            if (coupon.MinCartAmount > cart.ItemsTotalAmount)
            {
                throw new Exception("Coupon couldnt be applied to cart since cart does not meet the minimum amount criteria");
            }
            if (cart.CouponDiscountAmount > 0)
            {
                throw new Exception("A coupon is already applied to the cart");
            }

            var discountAmount = coupon.Type == DiscountType.Amount
                ? coupon.Amount
                : coupon.Amount * cart.ItemsTotalDiscountedAmount / 100;
            coupon.SetStatusInUse();
            cart.SetCouponDiscountAmount(discountAmount);
        }


        public void SetOrUpdateDeliveryCost()
        {
            var deliveryCost = DeliveryCostCalculatorService.CalculateDeliveryCost(cart);
            cart.SetDeliveryCost(deliveryCost);
        }


        public void AddItemToCart(string productTitle, int quantity)
        {
            if (quantity <= 0)
            {
                throw new Exception("Quantity of the product to add to cart must be positive number");
            }

            var product = ProductService.GetProductByTitle(productTitle);
            if (product == null)
            {
                throw new Exception($"Product with the title: {productTitle} does not exist");
            }

            cart.AddLineItem(new Item(product, quantity, quantity * product.Price));
        }

        private void UpdateCart()
        {
            var couponDiscountAmount = cart.GetCouponDiscount();
            cart.SetCouponDiscountAmount(0);
            ApplyOrUpdateCampaignsToCart();
            cart.SetCouponDiscountAmount(couponDiscountAmount);
            SetOrUpdateDeliveryCost();
        }

        public string GetCartInfo()
        {
            if (!cart.Items.Any())
            {
                return "Your cart is empty please add items using the command line. If you dont know how to do it you can use pleaseHelp command";
            }

            UpdateCart();
            string infoString = "";
            var itemsGroupedByCategory = cart.Items.GroupBy(i => i.Product.CategoryTitle);
            foreach (var items in itemsGroupedByCategory)
            {
                infoString+= "Category: " + items.Key;
                infoString+= "\n";
                foreach (var item in items)
                {
                    infoString += "Product Name:  " + item.Product.Title + "\t ";
                    infoString += "Quantity: " + item.Quantity + "\t ";
                    infoString += "Unit Price: " + item.Product.Price + "\t ";
                    infoString += "Total: " + item.TotalItemAmount + "\t ";
                    infoString += Math.Abs(item.TotalItemAmount - item.TotalDiscountedItemAmount) < 0.1 ? "": "Total Discounted Amount: " + item.TotalDiscountedItemAmount + "\t ";
                    infoString += "\n";
                }
            }
            infoString += "\n";
            infoString += "Total Campaign Discount: " + cart.GetCampaignDiscount() + "\t ";
            infoString += "Coupon Discount: " + cart.GetCouponDiscount() + "\t ";
            infoString += "\n";
            infoString += "Delivery Cost: " + cart.GetDeliveryCost() + "\t ";
            infoString += "Total Amount: " + cart.GetTotalCartAmount() + "\t ";
            infoString += "Total AmountWithDiscounts: " + cart.GetTotalCartAmountAfterDiscounts() + "\t ";
            infoString += "Total Amount With Delivery Cost: " + (cart.GetTotalCartAmountAfterDiscounts() + cart.GetDeliveryCost()) + "\t ";


            return infoString;
        }

        public Models.ShoppingCart GetCart()
        {
            return cart;
        }
    }
}
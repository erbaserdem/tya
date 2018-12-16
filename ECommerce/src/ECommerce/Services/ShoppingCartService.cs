using System;
using System.Linq;
using ECommerce.Models;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private IProductService ProductService;
        private ICampaignService CampaignService;
        private IDeliveryCostCalculatorService DeliveryCostCalculatorService;

        public ShoppingCartService(IProductService productService, IDeliveryCostCalculatorService deliveryCostCalculatorService, ICampaignService campaignService)
        {
            ProductService = productService;
            DeliveryCostCalculatorService = deliveryCostCalculatorService;
            CampaignService = campaignService;
        }

        public void ApplyOrUpdateCampaignsToCart(ShoppingCart cart)
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
                CampaignService.GetCampaignsByCategoryTitleAndMinimumQuantity(categoryTitle, cartItemQuantity);
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

        public void ApplyCouponToCart(ShoppingCart cart, Coupon coupon)
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


        public void SetOrUpdateDeliveryCost(ShoppingCart cart)
        {
            var deliveryCost = DeliveryCostCalculatorService.CalculateDeliveryCost(cart);
            cart.SetDeliveryCost(deliveryCost);
        }


        public void AddItemToCart(ShoppingCart cart, string productTitle, int quantity)
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
            ApplyOrUpdateCampaignsToCart(cart);
        }
    }
}
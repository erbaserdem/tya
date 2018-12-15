using System;
using ECommerce.Models;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services
{
    class ShoppingCartService : IShoppingCartService
    {
        private IProductService ProductService;
        private IDeliveryCostCalculatorService DeliveryCostCalculatorService;

        public ShoppingCartService(IProductService productService, IDeliveryCostCalculatorService deliveryCostCalculatorService)
        {
            ProductService = productService;
            DeliveryCostCalculatorService = deliveryCostCalculatorService;
        }

        public void ApplyDiscountsToCart()
        {
            throw new NotImplementedException();
        }

        public void ApplyCouponToCart(Coupon coupon)
        {
        }

        public void AddItemToCart(ShoppingCart cart, string productTitle, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
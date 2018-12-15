using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    interface IShoppingCartService
    {
        void ApplyDiscountsToCart();

        void ApplyCouponToCart(Coupon coupon);

        void AddItemToCart(ShoppingCart cart, string productTitle, int quantity);
    }

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
            throw new NotImplementedException();
        }

        public void AddItemToCart(ShoppingCart cart, string productTitle, int quantity)
        {
            if (quantity<=0)
            {
                throw new Exception($"Quantity of the product to add to cart must be positive number");
            }

            if (!ProductService.ProductExists(productTitle))
            {
                throw new Exception($"Product with the title: {productTitle} does not exist");
            }

            var product = ProductService.GetProductByTitle(productTitle);
            cart.AddLineItem(new Item(product, quantity, quantity*product.Price));
        }
    }
}

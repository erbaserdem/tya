using System;
using ECommerce.Models;
using ECommerce.Persistence;
using ECommerce.Persistence.Interfaces;
using ECommerce.Services;
using ECommerce.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce
{
    class Program
    {
        static void Main(string[] args)
        {
            double costPerDelivery = 5, costPerProduct = 6, fixedCost = 2.99;
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICampaignRepo, InMemoryCampaignRepo>()
                .AddSingleton<IProductRepo, InMemoryProductRepo>()
                .AddSingleton<ICategoryRepo, InMemoryCategoryRepo>()
                .AddSingleton<ICampaignService, CampaignService>()
                .AddSingleton<ICategoryService, CategoryService>()
                .AddSingleton<IProductService, ProductService>()
                .AddSingleton<IShoppingCartService, ShoppingCartService>()
                .AddSingleton<IDeliveryCostCalculatorService, DeliveryCostCalculatorService>(x =>
                    new DeliveryCostCalculatorService(costPerDelivery,
                        costPerProduct,
                        fixedCost))
                .BuildServiceProvider();


            var campaignService = serviceProvider.GetService<ICampaignService>();
            var categoryService = serviceProvider.GetService<ICategoryService>();
            var productService = serviceProvider.GetService<IProductService>();
            var cartService = serviceProvider.GetService<IShoppingCartService>();
            var deliveryCostCalculator = serviceProvider.GetService<IDeliveryCostCalculatorService>();


            Coupon coupon = new Coupon(1000, 5, DiscountType.Rate);
            categoryService.CreateCategory("aaa");
            productService.CreateProduct(245, "ALMOND","aaa");
            campaignService.CreateCampaign("aaa",20,3,DiscountType.Amount);
            ShoppingCart cart = new ShoppingCart();
            cartService.AddItemToCart(cart, "ALMOND", 5);
            cartService.ApplyOrUpdateCampaignsToCart(cart);
            cartService.ApplyCouponToCart(cart, coupon);
            cartService.SetOrUpdateDeliveryCost(cart);
            
        }
    }
}

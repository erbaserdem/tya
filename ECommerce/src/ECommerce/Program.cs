using System;
using System.Collections.Generic;
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
                .AddSingleton<ICouponService, CouponService>()
                .AddSingleton<InputProcessor>()
                .AddSingleton<IDeliveryCostCalculatorService, DeliveryCostCalculatorService>(x =>
                    new DeliveryCostCalculatorService(costPerDelivery,
                        costPerProduct,
                        fixedCost))
                .BuildServiceProvider();

            var ip = serviceProvider.GetService<InputProcessor>();
            ip.StartProcessing();

        }
    }
}

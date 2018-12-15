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
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICampaignRepo, InMemoryCampaignRepo>()
                .AddSingleton<IProductRepo, InMemoryProductRepo>()
                .AddSingleton<ICategoryRepo, InMemoryCategoryRepo>()
                .AddSingleton<ICampaignService, CampaignService>()
                .AddSingleton<ICategoryService, CategoryService>()
                .AddSingleton<IProductService, ProductService>()
                //.AddSingleton<IShoppingCartService, ShoppingCartService>()
                //.AddSingleton<IDeliveryCostCalculatorService, DeliveryCostCalculatorService>()
                .BuildServiceProvider();


            var campaignService = serviceProvider.GetService<ICampaignService>();
            var categoryService = serviceProvider.GetService<ICategoryService>();
            var productService = serviceProvider.GetService<IProductService>();



            categoryService.CreateCategory("aaa");
            productService.CreateProduct(245, "ALMOND","aaa");
            campaignService.CreateCampaign("aaa",20,3,DiscountType.Amount);
        }
    }
}

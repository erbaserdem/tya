using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Persistence;
using ShoppingCart.Persistence.Interfaces;
using ShoppingCart.Services;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart
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

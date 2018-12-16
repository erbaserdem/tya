using System;
using System.Collections.Generic;
using System.Globalization;
using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart
{
    class InputProcessor
    {
        private IProductService ProductService;
        private ICategoryService CategoryService;
        private ICampaignService CampaignService;
        private IShoppingCartService ShoppingCartService;
        private ICouponService CouponService;
        private const string EndInput = "quit";

        public InputProcessor(IProductService productService,
            ICategoryService categoryService,
            ICampaignService campaignService,
            IShoppingCartService shoppingCartService, ICouponService couponService)
        {
            ProductService = productService;
            CategoryService = categoryService;
            CampaignService = campaignService;
            ShoppingCartService = shoppingCartService;
            CouponService = couponService;
        }

        public void StartProcessing()
        {
            Console.WriteLine(
                "Welcome! \n While Creating Campaign and Coupon do not forget to enter discount type 0 for RATE and 1 for AMOUNT" +
                "\n Beware!! When you create a coupon it will be automatically applied to the cart.You must use , for double" +
                "\n Type pleaseHelp to see commands" +
                "\n If there are no exceptions your it means that your command has been processed successfully"+
                "\n Please enter your command");

            var consoleInput = Console.ReadLine();
            while (consoleInput != null && consoleInput != EndInput)
            {
                ExecuteCommand(consoleInput.Split(' '));
                Console.WriteLine("\n Please enter your command \n");
                consoleInput = Console.ReadLine();
            }

            Console.WriteLine("Bye Have A Great Time");
            Console.ReadLine();
        }

        private void ExecuteCommand(string[] input)
        {
            var command = input[0];
            switch (command)
            {
                case "createCategory":
                    ExecuteCreateCategoryCommand(input);
                    break;
                case "createProduct":
                    ExecuteCreateProductCommand(input);
                    break;
                case "createCampaign":
                    ExecuteCreateCampaignCommand(input);
                    break;
                case "addItemToCart":
                    ExecuteAddItemToCartCommand(input);
                    break;
                case "getCartInfo":
                    ExecuteGetCartInfoCommand();
                    break;
                case "addCoupon":
                    ExecuteAddAndApplyCouponCommand(input);
                    break;
                case "pleaseHelp":
                    PrintCommandExamples();
                    break;
                default:
                    Console.WriteLine("Not a valid command");
                    break;

            }
        }

        private void PrintCommandExamples()
        {
            var commands = new List<string>
            {
                "createCategory <categoryTitle> <ParentCategory(Optional)>",
                "createProduct <price> <productTitle> <categoryTitle>",
                "createCampaign <categoryTitle> <amount> <minItemCount> <DiscountType(1 for amount, 0 for rate)>",
                "addItemToCart <productTitle> <itemQuantity>",
                "getCartInfo",
                "addCoupon <minAmount> <amount> <DiscountType(1 for amount, 0 for rate)>"
            };
            foreach (var command in commands)
            {
                Console.WriteLine($"\n {command}");
            }
            Console.WriteLine("\n");
        }

        private void ExecuteAddAndApplyCouponCommand(string[] input)
        {
            try
            {
                double minAmount = ParseToDouble(input[1]);
                double amount = ParseToDouble(input[2]);
                DiscountType type = (DiscountType)Int32.Parse(input[3]);
                var coupon = CouponService.CreateCoupon(minAmount, amount, type);
                ShoppingCartService.ApplyCouponToCart(coupon);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ExecuteGetCartInfoCommand()
        {
            Console.WriteLine(ShoppingCartService.GetCartInfo());
        }

        private void ExecuteAddItemToCartCommand(string[] input)
        {
            try
            {
                var productTitle = input[1];
                int itemQuantity = Int32.Parse(input[2]);
                ShoppingCartService.AddItemToCart(productTitle, itemQuantity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ExecuteCreateCampaignCommand(string[] input)
        {
            try
            {
                var categoryTitle = input[1];
                double amount = ParseToDouble(input[2]);
                int minItemCount = Int32.Parse(input[3]);
                DiscountType type = (DiscountType)Int32.Parse(input[4]);
                CampaignService.CreateCampaign(categoryTitle, amount, minItemCount, type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ExecuteCreateCategoryCommand(string[] input)
        {
            try
            {
                var categoryTitle = input[1];
                var parentCategory = input.Length == 3 ? input[2] : null;
                CategoryService.CreateCategory(categoryTitle, parentCategory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ExecuteCreateProductCommand(string[] input)
        {
            try
            {
                double price = ParseToDouble(input[1]);
                var title = input[2];
                var categoryTitle = input[3];
                ProductService.CreateProduct(price, title, categoryTitle);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private double ParseToDouble(string value)
        {
            return double.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}

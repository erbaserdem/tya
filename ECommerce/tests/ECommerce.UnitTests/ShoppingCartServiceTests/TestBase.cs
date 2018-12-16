using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models;
using ECommerce.Services;
using ECommerce.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace ECommerce.UnitTests.ShoppingCartServiceTests
{
    public abstract class TestBase
    {
        protected readonly Mock<IProductService> productService = new Mock<IProductService>();
        protected readonly Mock<IDeliveryCostCalculatorService> deliveryCostCalculatorService = new Mock<IDeliveryCostCalculatorService>();
        protected readonly Mock<ICampaignService> campaignService = new Mock<ICampaignService>();
        protected readonly Mock<ICategoryService> categoryService= new Mock<ICategoryService>();
        protected Product productToAddCart;
        protected ShoppingCart cart = new ShoppingCart();
        protected ShoppingCartService cartService;
        protected Campaign rateTypeCampaign, amountTypeCampaign;
        protected decimal rateTypeDiscountAmount = 15, amountTypeDiscountAmount = 15;
        protected string ParentCategory= "Parent";
        protected IList<Campaign> campaigns;
        protected readonly int quantity = 15;


        [OneTimeSetUp]
        public virtual void Setup()
        {
            cartService = new ShoppingCartService(productService.Object, deliveryCostCalculatorService.Object, campaignService.Object);
            SetUpData();
            SetUpMocks();
        }

        protected virtual void SetUpData()
        {
            productToAddCart = new Product(90m, "productTitle", ParentCategory);
            rateTypeCampaign = new Campaign(ParentCategory, rateTypeDiscountAmount, quantity - 10, DiscountType.Rate);
            amountTypeCampaign = new Campaign(ParentCategory, amountTypeDiscountAmount, quantity - 10, DiscountType.Amount);
            campaigns = new List<Campaign>
            {
                rateTypeCampaign,
                amountTypeCampaign
            };
        }

        protected virtual void SetUpMocks()
        {
            productService.Setup(p => p.GetProductByTitle(productToAddCart.Title)).Returns(productToAddCart);
            campaignService.Setup(a =>
                a.GetCampaignsByCategoryTitleAndMinimumQuantity(productToAddCart.CategoryTitle, quantity)).Returns(campaigns);
        }
    }
}

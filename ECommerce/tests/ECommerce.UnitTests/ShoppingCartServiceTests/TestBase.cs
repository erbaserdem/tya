using System;
using System.Collections;
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
        protected Product productToAddCart;
        protected ShoppingCartService cartService;
        protected Campaign rateTypeCampaign, amountTypeCampaign;
        protected double rateTypeDiscountAmount = 15, amountTypeDiscountAmount = 15;

        protected readonly string ParentCategory = "Parent",
            ChildCategoryLevel1 = "ChildCategoryLevel1",
            ChildCategoryLevel2 = "ChildCategoryLevel2",
            NoDiscount = "NoDiscount";
        protected string ProductCategory,
            RateTypedDiscountCategory,
            AmountTypedDiscountCategory;
        protected IList<Campaign> Campaigns;
        protected readonly int Quantity = 15;


        [OneTimeSetUp]
        public virtual void Setup()
        {
            ProductCategory = ParentCategory;
            RateTypedDiscountCategory = ParentCategory;
            AmountTypedDiscountCategory = ParentCategory;
            cartService = new ShoppingCartService(productService.Object, deliveryCostCalculatorService.Object, campaignService.Object);
            SetUpData();
            SetUpMocks();
        }

        protected virtual void SetUpData()
        {
            rateTypeCampaign = new Campaign(RateTypedDiscountCategory, rateTypeDiscountAmount, Quantity - 10, DiscountType.Rate);
            amountTypeCampaign = new Campaign(AmountTypedDiscountCategory, amountTypeDiscountAmount, Quantity - 10, DiscountType.Amount);
            Campaigns = new List<Campaign>
            {
                rateTypeCampaign,
                amountTypeCampaign
            };
            productToAddCart = new Product(90, "productTitle", ProductCategory);
        }

        protected virtual void SetUpMocks()
        {
            productService.Setup(p => p.GetProductByTitle(productToAddCart.Title)).Returns(productToAddCart);
            campaignService.Setup(a =>a.GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(ChildCategoryLevel2, Quantity)).Returns(Campaigns);
            campaignService.Setup(a =>a.GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(ChildCategoryLevel1, Quantity)).Returns(Campaigns);
            campaignService.Setup(a =>a.GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(ProductCategory, Quantity)).Returns(Campaigns);
            campaignService.Setup(a =>a.GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(NoDiscount, Quantity)).Returns((IEnumerable<Campaign>)null);
        }
    }
}

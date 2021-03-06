using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ShoppingCart.Models;
using ShoppingCart.Persistence;
using ShoppingCart.Persistence.Interfaces;
using ShoppingCart.Services;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.UnitTests.CampaingServiceTests
{
    public class when_creating_a_campaign_with_incompatible_product
    {
        private string categoryTitle = "CategoryTitle";
        private double discountAmount = 35;
        private DiscountType discountType = DiscountType.Amount;
        private int discountMinItemCount = 5;
        private ICampaignRepo campaignRepo = new InMemoryCampaignRepo();
        private Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
        private Mock<IProductService> productService= new Mock<IProductService>();
        private ICampaignService campaignService;
        private Product productWithHigerPriceThanDiscountAmount;
        private Action action;



        [OneTimeSetUp]
        public void Setup()
        {
            SetUpMocks();
            campaignService = new CampaignService(campaignRepo, categoryService.Object, productService.Object);

            action = ()=>
            {
                campaignService.CreateCampaign(categoryTitle, discountAmount, discountMinItemCount, discountType);
            };
        }

        [Test]
        public void it_should_throw_exception_due_to_product_incompability()
        {
            action.Should().Throw<Exception>();
        }

        private void SetUpMocks()
        {
            categoryService.Setup(c => c.CategoryExists(categoryTitle))
                .Returns(true);

            productWithHigerPriceThanDiscountAmount = new Product(discountAmount-10, "someTitle", categoryTitle);

            productService.Setup(p => p.GetProductsByCategoryTitle(categoryTitle)).Returns(new List<Product> { productWithHigerPriceThanDiscountAmount });
        }

    }
}
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
    public class when_creating_a_campaign_with_invalid_amount
    {
        private string categoryTitle = "CategoryTitle";
        private DiscountType discountType = DiscountType.Rate;
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
        }

        [TestCase(100)]
        [TestCase(200)]
        [TestCase(00)]
        [TestCase(-24)]
        public void it_should_throw_exception_due_to_invalid_amount(double amount)
        {
            action = () =>
            {
                campaignService.CreateCampaign(categoryTitle, amount, discountMinItemCount, discountType);
            };
            action.Should().Throw<Exception>();
        }

        private void SetUpMocks()
        {
            categoryService.Setup(c => c.CategoryExists(categoryTitle))
                .Returns(true);

            productWithHigerPriceThanDiscountAmount = new Product(1010, "someTitle", categoryTitle);

            productService.Setup(p => p.GetProductsByCategoryTitle(categoryTitle)).Returns(new List<Product> { productWithHigerPriceThanDiscountAmount });
        }

    }
}
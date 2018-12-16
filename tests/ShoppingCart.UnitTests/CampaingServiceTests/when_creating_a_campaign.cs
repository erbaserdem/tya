using System.Collections.Generic;
using System.Linq;
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
    public class when_creating_a_campaign
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



        [OneTimeSetUp]
        public void Setup()
        {
            SetUpMocks();
            campaignService = new CampaignService(campaignRepo, categoryService.Object, productService.Object);
            campaignService.CreateCampaign(categoryTitle, discountAmount, discountMinItemCount, discountType);
        }

        [Test]
        public void it_should_create_campaign_with_given_parameters()
        {
            var campaigns = campaignService.GetCampaignsByCategoryTitle(categoryTitle);
            campaigns.Count().Should().Be(1);
            var campaign = campaigns.First();
            campaign.CategoryTitle.Should().Be(categoryTitle);
            campaign.Type.Should().Be(discountType);
            campaign.Amount.Should().Be(discountAmount);
            campaign.MinItemQty.Should().Be(discountMinItemCount);
        }

        private void SetUpMocks()
        {
            categoryService.Setup(c => c.CategoryExists(categoryTitle))
                .Returns(true);

            productWithHigerPriceThanDiscountAmount = new Product(discountAmount+10, "someTitle", categoryTitle);

            productService.Setup(p => p.GetProductsByCategoryTitle(categoryTitle)).Returns(new List<Product> { productWithHigerPriceThanDiscountAmount });
        }

    }
}
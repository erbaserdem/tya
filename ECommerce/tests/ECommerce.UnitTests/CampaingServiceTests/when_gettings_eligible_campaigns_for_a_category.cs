using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Persistence;
using ECommerce.Persistence.Interfaces;
using ECommerce.Services;
using ECommerce.Services.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ECommerce.UnitTests.CampaingServiceTests
{
    public class when_gettings_eligible_campaigns_for_a_category
    {
        protected readonly string ParentCategory = "Parent",
            ChildCategoryLevel1 = "ChildCategoryLevel1",
            ChildCategoryLevel2 = "ChildCategoryLevel2",
            NoDiscount = "NoDiscount";
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
            campaignService.CreateCampaign(ParentCategory, discountAmount, discountMinItemCount, discountType);
        }

        [Test]
        public void it_should_return_parent_category_campaign_if_none_fourn_for_main_category()
        {
            var campaigns = campaignService.GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(ChildCategoryLevel2,discountMinItemCount+5);
            campaigns.Count().Should().Be(1);
            var campaign = campaigns.First();
            campaign.CategoryTitle.Should().Be(ParentCategory);
            campaign.Type.Should().Be(discountType);
            campaign.Amount.Should().Be(discountAmount);
            campaign.MinItemQty.Should().Be(discountMinItemCount);
        }

        private void SetUpMocks()
        {
            categoryService.Setup(c => c.CategoryExists(ParentCategory))
                .Returns(true);

            categoryService.Setup(a => a.GetParentCategoryTitle(ChildCategoryLevel2)).Returns(ChildCategoryLevel1);
            categoryService.Setup(a => a.GetParentCategoryTitle(ChildCategoryLevel1)).Returns(ParentCategory);

            productWithHigerPriceThanDiscountAmount = new Product(discountAmount+10, "someTitle", ParentCategory);

            productService.Setup(p => p.GetProductsByCategoryTitle(ParentCategory)).Returns(new List<Product> { productWithHigerPriceThanDiscountAmount });
        }

    }
}
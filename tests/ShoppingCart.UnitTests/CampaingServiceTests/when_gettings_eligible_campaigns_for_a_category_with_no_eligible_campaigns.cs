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
    public class when_gettings_eligible_campaigns_for_a_category_with_no_eligible_campaigns
    {
        protected readonly string categoryTitle = "CategoryTitle",
            ParentCategory = "Parent",
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
        private Product product;



        [OneTimeSetUp]
        public void Setup()
        {
            SetUpMocks();
            campaignService = new CampaignService(campaignRepo, categoryService.Object, productService.Object);
            campaignService.CreateCampaign(categoryTitle, discountAmount, discountMinItemCount, discountType);
        }

        [Test]
        public void it_should_not_be_able_to_find_any_categories_for_given_product()
        {
            var campaigns = campaignService.GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(ChildCategoryLevel2,discountMinItemCount+5);
            campaigns.Should().BeNull();
        }

        private void SetUpMocks()
        {
            categoryService.Setup(c => c.CategoryExists(ParentCategory))
                .Returns(true);
            categoryService.Setup(c => c.CategoryExists(categoryTitle))
                .Returns(true);

            categoryService.Setup(a => a.GetParentCategoryTitle(ChildCategoryLevel2)).Returns(ChildCategoryLevel1);
            categoryService.Setup(a => a.GetParentCategoryTitle(ChildCategoryLevel1)).Returns(ParentCategory);

            product = new Product(discountAmount+10, "someTitle", ParentCategory);

            productService.Setup(p => p.GetProductsByCategoryTitle(ParentCategory)).Returns(new List<Product> { product });
        }

    }
}
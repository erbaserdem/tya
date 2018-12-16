using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Persistence.Interfaces;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services
{
    public class CampaignService : ICampaignService
    {
        private ICampaignRepo campaignRepo;
        private ICategoryService categoryService;
        private IProductService productService;

        public CampaignService(ICampaignRepo campaignRepo, ICategoryService categoryService, IProductService productService)
        {
            this.campaignRepo = campaignRepo;
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public void CreateCampaign(string categoryTitle, double amount, int minItemCount, DiscountType type)
        {
            EnsureCampaignIsValid(categoryTitle, amount, minItemCount, type);
            campaignRepo.CreateCampaign(new Campaign(categoryTitle, amount, minItemCount, type));
        }

        public IEnumerable<Campaign> GetCampaignsByCategoryTitle(string categoryTitle)
        {
            return campaignRepo.GetCampaignsByCategoryTitle(categoryTitle);
        }

        public IEnumerable<Campaign> GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(string categoryTitle, int minQty)
        {
            var campaigns = campaignRepo.GetCampaignsByCategoryTitleAndMinimumQuantity(categoryTitle, minQty);
            if (campaigns.Any())
            {
                return campaigns;
            }

            var parentCategoryTitle = categoryService.GetParentCategoryTitle(categoryTitle);
            if (parentCategoryTitle == null)
            {
                return null;
            }

            return GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(parentCategoryTitle, minQty);
        }

        private void EnsureCampaignIsValid(string categoryTitle, double amount, int minItemCount, DiscountType type)
        {
            if (!categoryService.CategoryExists(categoryTitle))
            {
                throw new Exception($"Category with title: {categoryTitle} does not exists");
            }
            if (type == DiscountType.Rate && (amount>=100 || amount<=0))
            {
                throw new Exception($"Discount amount should be between 0 and 100 for rate typed discounts");
            }
            if (minItemCount<0)
            {
                throw new Exception($"Min item count should be zero or a positive number");
            }
            if (type == DiscountType.Amount)
            {
                var products = productService.GetProductsByCategoryTitle(categoryTitle);
                if (products.Any(p => p.Price < amount))
                {
                    throw new Exception($"Category {categoryTitle} contains a product which has a lower price than the discount amount: {amount}");
                }
            }
        }
    }
}
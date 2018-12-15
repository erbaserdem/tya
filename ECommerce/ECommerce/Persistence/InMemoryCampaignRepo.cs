﻿using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Persistence.Interfaces;

namespace ECommerce.Persistence
{
    public class InMemoryCampaignRepo : List<Campaign>, ICampaignRepo
    {
        public IEnumerable<Campaign> GetCampaignsByCategoryTitle(string title)
        {
            return this.Where(c => c.CategoryTitle == title);
        }

        public IEnumerable<Campaign> GetCampaignsByCategoryTitleAndMinimumQuantity(string categoryTitle, int minQty)
        {
            return this.Where(c => c.CategoryTitle == categoryTitle && c.MinItemQty <= minQty);
        }

        public void CreateCampaign(Campaign campaign)
        {
            this.Add(campaign);
        }
    }
}
﻿using System.Collections.Generic;
using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
    public interface ICampaignService
    {
        void CreateCampaign(string categoryTitle, double amount, int minItemCount, DiscountType type);
        IEnumerable<Campaign> GetCampaignsByCategoryTitle(string categoryTitle);
        IEnumerable<Campaign> GetEligibleCampaignsByCategoryTitleAndMinimumQuantity(string categoryTitle, int minQty);

        //should it prevent duplicate campaign creation??
        //bool CampaignExists(string categoryTitle, int minItemCount);
    }
}

using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface ICampaignService
    {
        void CreateCampaign(string categoryTitle, decimal amount, int minItemCount, DiscountType type);
        IEnumerable<Campaign> GetCampaignsByCategoryTitle(string categoryTitle);
        IEnumerable<Campaign> GetCampaignsByCategoryTitleAndMinimumQuantity(string categoryTitle, int minQty);

        //should it prevent duplicate campaign creation??
        //bool CampaignExists(string categoryTitle, int minItemCount);
    }
}

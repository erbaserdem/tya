using System.Collections.Generic;
using ShoppingCart.Models;

namespace ShoppingCart.Persistence.Interfaces
{
    public interface ICampaignRepo
    {
        IEnumerable<Campaign> GetCampaignsByCategoryTitle(string title);
        IEnumerable<Campaign> GetCampaignsByCategoryTitleAndMinimumQuantity(string categoryTitle, int minQty);
        void CreateCampaign(Campaign campaign);
    }
}

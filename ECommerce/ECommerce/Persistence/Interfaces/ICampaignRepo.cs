using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Persistence.Interfaces
{
    public interface ICampaignRepo
    {
        IEnumerable<Campaign> GetCampaignsByCategoryTitle(string title);
        IEnumerable<Campaign> GetCampaignsByCategoryTitleAndMinimumQuantity(string categoryTitle, int minQty);
        void CreateCampaign(Campaign campaign);
    }
}

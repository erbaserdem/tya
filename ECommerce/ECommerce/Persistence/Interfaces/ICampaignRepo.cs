using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Persistence.Interfaces
{
    public interface ICampaignRepo
    {
        IEnumerable<Campaign> GetCampaignsByCategoryTitle(string title);
        void CreateCampaign(Campaign campaign);
    }
}

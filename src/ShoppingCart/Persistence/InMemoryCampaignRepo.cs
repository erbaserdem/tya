using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Models;
using ShoppingCart.Persistence.Interfaces;

namespace ShoppingCart.Persistence
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
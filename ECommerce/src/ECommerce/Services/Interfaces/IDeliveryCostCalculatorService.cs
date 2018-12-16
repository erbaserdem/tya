using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IDeliveryCostCalculatorService
    {
        double CalculateDeliveryCost(ShoppingCart cart);
    }
}

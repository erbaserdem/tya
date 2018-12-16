using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IDeliveryCostCalculatorService
    {
        decimal CalculateDeliveryCost(ShoppingCart cart);
    }
}

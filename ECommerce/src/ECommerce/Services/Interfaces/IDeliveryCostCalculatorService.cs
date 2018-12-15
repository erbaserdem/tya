using ECommerce.Models;

namespace ECommerce.Services
{
    public interface IDeliveryCostCalculatorService
    {
        decimal CostPerDelivery { get; set; }
        decimal CostPerProduct { get; set; }
        decimal FixedCost { get; set; }

        double CalculateDeliveryCost(ShoppingCart cart);
    }
}

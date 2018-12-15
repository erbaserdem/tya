using ECommerce.Models;

namespace ECommerce.Services
{
    interface IDeliveryCostCalculatorService
    {
        decimal CostPerDelivery { get; set; }
        decimal CostPerProduct { get; set; }
        decimal FixedCost { get; set; }

        double CalculateDeliveryCost(ShoppingCart cart);
    }
}

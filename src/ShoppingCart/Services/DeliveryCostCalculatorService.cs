using System.Linq;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.Services
{
    class DeliveryCostCalculatorService : IDeliveryCostCalculatorService
    {
        public DeliveryCostCalculatorService(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }

        public double CostPerDelivery { get; set; }
        public double CostPerProduct { get; set; }
        public double FixedCost { get; set; }
        public double CalculateDeliveryCost(Models.ShoppingCart cart)
        {
            if (!cart.Items.Any())
            {
                return 0;
            }

            var numberOfDeliveries = cart.Items.Select(i => i.Product.CategoryTitle).Distinct().Count();
            var numberOfProducts = cart.Items.Select(i => i.Product.Title).Distinct().Count();

            return (numberOfDeliveries*CostPerDelivery) + (numberOfProducts * CostPerProduct) +FixedCost;
        }
    }
}
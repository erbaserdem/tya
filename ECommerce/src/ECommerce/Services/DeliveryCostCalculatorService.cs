using System.Linq;
using ECommerce.Models;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services
{
    class DeliveryCostCalculatorService : IDeliveryCostCalculatorService
    {
        public DeliveryCostCalculatorService(decimal costPerDelivery, decimal costPerProduct, decimal fixedCost)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }

        public decimal CostPerDelivery { get; set; }
        public decimal CostPerProduct { get; set; }
        public decimal FixedCost { get; set; }
        public decimal CalculateDeliveryCost(ShoppingCart cart)
        {
            var numberOfDeliveries = cart.Items.Select(i => i.Product.CategoryTitle).Distinct().Count();
            var numberOfProducts = cart.Items.Select(i => i.Product.Title).Distinct().Count();

            return (numberOfDeliveries*CostPerDelivery) + (numberOfProducts * CostPerProduct) +FixedCost;
        }
    }
}
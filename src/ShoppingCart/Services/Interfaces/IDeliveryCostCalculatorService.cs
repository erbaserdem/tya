namespace ShoppingCart.Services.Interfaces
{
    public interface IDeliveryCostCalculatorService
    {
        double CalculateDeliveryCost(Models.ShoppingCart cart);
    }
}

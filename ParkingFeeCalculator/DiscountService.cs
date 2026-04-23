namespace ParkingFeeCalculator;

public interface IDiscountService //
{
    double GetDiscount();
}

public class DiscountService : IDiscountService
{
    public double GetDiscount() => 0.9;
}

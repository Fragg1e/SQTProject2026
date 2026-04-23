namespace ParkingFeeCalculator;

public interface IParkingService 
{
    double CalculateFee(int hours, string vehicleType);
}

public class ParkingService : IParkingService
{
    private readonly IDiscountService _discountService;

    public ParkingService(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    public double CalculateFee(int hours, string vehicleType) //calculates parking fee based on hours and vehicle type
    {
        if (hours <= 0 || string.IsNullOrWhiteSpace(vehicleType))
        {
            return 0.0;
        }

        var normalizedVehicleType = vehicleType.Trim().ToLowerInvariant(); //normalizes vehicle type input to handle case and whitespace issues
        var fee = normalizedVehicleType switch
        {
            "standard" => CalculateStandardFee(hours),
            "electric" => CalculateElectricFee(hours),
            _ => 0.0
        };

        if (fee <= 0 || hours < 10)
        {
            return fee;
        }

        return fee * _discountService.GetDiscount();
    }

    private static double CalculateStandardFee(int hours) 
    {
        if (hours is >= 1 and <= 3)
        {
            return hours * 4.0;
        }

        return hours >= 4 ? hours * 3.0 : 0.0;
    }

    private static double CalculateElectricFee(int hours)
    {
        if (hours is >= 1 and <= 5)
        {
            return hours * 3.0;
        }

        return hours >= 6 ? hours * 2.0 : 0.0;
    }
}

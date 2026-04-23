using Moq;
using ParkingFeeCalculator;

namespace ParkingFeeCalculator.Tests;

[TestFixture]
public class ParkingServiceTests
{
    [TestCase(1, "standard", 4.0)]
    [TestCase(3, "standard", 12.0)]
    [TestCase(4, "standard", 12.0)]
    [TestCase(9, "standard", 27.0)]
    [TestCase(1, "electric", 3.0)]
    [TestCase(5, "electric", 15.0)]
    [TestCase(6, "electric", 12.0)]
    [TestCase(9, "electric", 18.0)]
    public void Calculates_the_normal_parking_fee(int hours, string vehicleType, double expected)
    {
        var service = MakeService();

        var result = service.CalculateFee(hours, vehicleType);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Vehicle_type_does_not_have_to_match_the_case_exactly()
    {
        var service = MakeService();

        var result = service.CalculateFee(6, "  EleCTric  ");

        Assert.That(result, Is.EqualTo(12.0));
    }

    [TestCase(10, "standard", 27.0)]
    [TestCase(10, "electric", 18.0)]
    public void Ten_hours_or_more_gets_the_discount(int hours, string vehicleType, double expected)
    {
        var fakeDiscount = new Mock<IDiscountService>();
        fakeDiscount.Setup(x => x.GetDiscount()).Returns(0.9);

        var service = new ParkingService(fakeDiscount.Object);

        var result = service.CalculateFee(hours, vehicleType);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void Bad_hours_return_zero(int hours)
    {
        var service = MakeService();

        var result = service.CalculateFee(hours, "standard");

        Assert.That(result, Is.EqualTo(0.0));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("motorbike")]
    public void Bad_vehicle_types_return_zero(string vehicleType)
    {
        var service = MakeService();

        var result = service.CalculateFee(4, vehicleType);

        Assert.That(result, Is.EqualTo(0.0));
    }

    [Test]
    public void Discount_service_is_needed()
    {
        Assert.That(() => new ParkingService(null!), Throws.ArgumentNullException);
    }

    private static ParkingService MakeService()
    {
        var discount = new Mock<IDiscountService>();
        discount.Setup(x => x.GetDiscount()).Returns(0.9);

        return new ParkingService(discount.Object);
    }
}

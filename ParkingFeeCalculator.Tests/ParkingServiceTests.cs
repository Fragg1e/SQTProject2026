using Moq;
using ParkingFeeCalculator;

namespace ParkingFeeCalculator.Tests;

[TestFixture]
public class ParkingServiceTests
{
    [TestCase(1, 4.0)]
    [TestCase(3, 12.0)]
    [TestCase(4, 12.0)]
    [TestCase(9, 27.0)]
    public void CalculateFee_ReturnsExpectedFee_ForStandardVehiclesUnderDiscountThreshold(int hours, double expectedFee)
    {
        var discountService = new Mock<IDiscountService>(MockBehavior.Strict);
        var sut = new ParkingService(discountService.Object);

        var result = sut.CalculateFee(hours, "standard");

        Assert.That(result, Is.EqualTo(expectedFee));
        discountService.Verify(ds => ds.GetDiscount(), Times.Never);
    }

    [TestCase(1, 3.0)]
    [TestCase(5, 15.0)]
    [TestCase(6, 12.0)]
    [TestCase(9, 18.0)]
    public void CalculateFee_ReturnsExpectedFee_ForElectricVehiclesUnderDiscountThreshold(int hours, double expectedFee)
    {
        var discountService = new Mock<IDiscountService>(MockBehavior.Strict);
        var sut = new ParkingService(discountService.Object);

        var result = sut.CalculateFee(hours, "electric");

        Assert.That(result, Is.EqualTo(expectedFee));
        discountService.Verify(ds => ds.GetDiscount(), Times.Never);
    }

    [Test]
    public void CalculateFee_IsCaseInsensitive_AndTrimsWhitespace()
    {
        var discountService = new Mock<IDiscountService>(MockBehavior.Strict);
        var sut = new ParkingService(discountService.Object);

        var result = sut.CalculateFee(6, "  EleCTric  ");

        Assert.That(result, Is.EqualTo(12.0));
    }

    [Test]
    public void CalculateFee_AppliesDiscount_WhenHoursAreTenOrMore()
    {
        var discountService = new Mock<IDiscountService>();
        discountService.Setup(ds => ds.GetDiscount()).Returns(0.9);
        var sut = new ParkingService(discountService.Object);

        var result = sut.CalculateFee(10, "standard");

        Assert.That(result, Is.EqualTo(27.0));
        discountService.Verify(ds => ds.GetDiscount(), Times.Once);
    }

    [Test]
    public void CalculateFee_AppliesDiscount_ForElectricVehicles_WhenHoursAreTenOrMore()
    {
        var discountService = new Mock<IDiscountService>();
        discountService.Setup(ds => ds.GetDiscount()).Returns(0.9);
        var sut = new ParkingService(discountService.Object);

        var result = sut.CalculateFee(10, "electric");

        Assert.That(result, Is.EqualTo(18.0));
        discountService.Verify(ds => ds.GetDiscount(), Times.Once);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void CalculateFee_ReturnsZero_ForInvalidHours(int hours)
    {
        var discountService = new Mock<IDiscountService>(MockBehavior.Strict);
        var sut = new ParkingService(discountService.Object);

        var result = sut.CalculateFee(hours, "standard");

        Assert.That(result, Is.EqualTo(0.0));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("motorbike")]
    public void CalculateFee_ReturnsZero_ForInvalidVehicleType(string vehicleType)
    {
        var discountService = new Mock<IDiscountService>(MockBehavior.Strict);
        var sut = new ParkingService(discountService.Object);

        var result = sut.CalculateFee(4, vehicleType);

        Assert.That(result, Is.EqualTo(0.0));
        discountService.Verify(ds => ds.GetDiscount(), Times.Never);
    }

    [Test]
    public void Constructor_ThrowsArgumentNullException_WhenDiscountServiceIsMissing()
    {
        Assert.That(() => new ParkingService(null!), Throws.ArgumentNullException);
    }
}

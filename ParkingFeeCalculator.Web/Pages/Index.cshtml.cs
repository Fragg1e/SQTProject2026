using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingFeeCalculator;

namespace ParkingFeeCalculator.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IParkingService _parkingService;

    public IndexModel(IParkingService parkingService)
    {
        _parkingService = parkingService;
    }

    [BindProperty]
    public ParkingFeeInputModel Input { get; set; } = new();

    public bool HasCalculated { get; private set; }

    public string ResultText { get; private set; } = "EUR0.00";

    public void OnGet()
    {
    }

    public void OnPost()
    {
        HasCalculated = true;
        var fee = _parkingService.CalculateFee(Input.HoursParked, Input.VehicleType ?? string.Empty);
        ResultText = $"EUR{fee:0.00}";
    }

    public sealed class ParkingFeeInputModel
    {
        [Display(Name = "Hours Parked")]
        public int HoursParked { get; set; }

        [Display(Name = "Vehicle Type")]
        public string? VehicleType { get; set; }
    }
}

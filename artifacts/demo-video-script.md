# Demo Video Script

Target length: 3 to 5 minutes.

## 1. Project Overview

Open `SQTProject2026Classic.sln`.

Explain that the solution contains:

- `ParkingFeeCalculator` as the system under test.
- `ParkingFeeCalculator.Tests` for NUnit and Moq unit tests.
- `ParkingFeeCalculator.Web` for the Razor Pages front end.
- `ParkingFeeCalculator.SeleniumTests` for Selenium WebDriver UI tests.
- `artifacts` for the written analysis.

## 2. Refactoring Decisions

Show `ParkingService.cs`.

Say:

The original code directly created `DiscountService` inside the calculation method.
I refactored that by introducing `IDiscountService` and injecting it through the `ParkingService` constructor.
This made the discount part easier to test with Moq.

Also mention:

- Vehicle type is trimmed and converted to lower case.
- Invalid hours and blank vehicle types return EUR0.00.
- Standard and electric pricing are split into helper methods.

## 3. Unit Testing Strategy

Show `ParkingServiceTests.cs`.

Say:

The unit tests cover the main branches and the main black-box cases. They check standard cases, electric cases, invalid input, case-insensitive input, and the discount threshold at 10 hours.

Point out:

- The normal tests just check the fee that comes back.
- The discount tests use a simple Moq setup to return `0.9`.
- Constructor validation is tested by passing a null dependency.

Run:

```powershell
dotnet test .\ParkingFeeCalculator.Tests\ParkingFeeCalculator.Tests.csproj
```

## 4. Web Front End And Selenium

Show the Razor Pages form in `Index.cshtml`.

Say:

The front end is a simple form where the user enters hours parked and vehicle type. The Selenium project starts the web application, fills in the form, submits it, and checks the result shown on the page.

Show the Selenium test cases:

- Standard vehicle boundary.
- Standard discount threshold.
- Electric vehicle boundary.
- Invalid vehicle type.
- Invalid hours.

## 5. Metrics Interpretation

Show `artifacts/metrics-report-human.docx` or the markdown version.

Say:

The metrics are okay for a small assignment project. `CalculateFee` has the highest complexity because it contains the main decisions. Coupling is lower after refactoring because `ParkingService` depends on `IDiscountService` instead of creating `DiscountService` itself.

## 6. Closing

Say:

The solution implements the required parking rules, refactors the discount dependency for testability, includes automated NUnit and Selenium tests, and includes the written white-box, black-box, and metrics analysis needed for submission.

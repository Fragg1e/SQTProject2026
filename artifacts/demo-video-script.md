# Demo Video Script

Target length: 3 to 5 minutes.

## 1. Project Overview

Open `SQTProject2026Classic.sln`.

Explain that the solution contains:

- `ParkingFeeCalculator` as the system under test.
- `ParkingFeeCalculator.Tests` for NUnit and Moq unit tests.
- `ParkingFeeCalculator.Web` for the Razor Pages front end.
- `ParkingFeeCalculator.SeleniumTests` for Selenium WebDriver UI tests.
- `azure-pipelines.yml` for CI.
- `artifacts` for the written analysis.

## 2. Refactoring Decisions

Show `ParkingService.cs`.

Say:

The original code directly created `DiscountService` inside the calculation method. 
I refactored that by introducing `IDiscountService` and injecting it through the `ParkingService` constructor. 
This decouples the discount rule from the fee calculation and lets the tests mock the discount service with Moq.

Also mention:

- Vehicle type is trimmed and converted to lower case to satisfy the case-insensitive requirement.
- Invalid hours and blank vehicle types return EUR0.00.
- Standard and electric pricing are separated into helper methods for readability.

## 3. Unit Testing Strategy

Show `ParkingServiceTests.cs`.

Say:

The unit tests cover both white-box branches and black-box pricing cases. They check standard vehicle boundaries, electric vehicle boundaries, invalid input, case-insensitive input, and the discount threshold at 10 hours.

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

The front end provides a simple form for hours parked and vehicle type. The Selenium project starts the web application, fills in the form, submits it, and verifies the displayed result.

Show the Selenium test cases:

- Standard vehicle boundary.
- Standard discount threshold.
- Electric vehicle boundary.
- Invalid vehicle type.
- Invalid hours.

## 5. CI Pipeline

Show `azure-pipelines.yml`.

Say:

The Azure DevOps pipeline uses a Windows agent, restores the solution, builds it, runs NUnit unit tests, starts the web app, runs Selenium tests, and publishes test results.

## 6. Metrics Interpretation

Show `artifacts/metrics-report.md`.

Say:

The metrics are acceptable for a small assignment solution. `CalculateFee` has the highest cyclomatic complexity because it contains the core decision logic. Coupling is low because `ParkingService` depends on the `IDiscountService` abstraction rather than directly constructing `DiscountService`. The classes are cohesive, and the inheritance depth is shallow.

## 7. Closing

Say:

The solution implements the required parking rules, refactors the discount dependency for testability, includes automated NUnit and Selenium tests, provides a CI pipeline, and includes the written white-box, black-box, and metrics analysis needed for submission.

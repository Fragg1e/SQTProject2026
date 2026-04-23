# SQTProject2026

Software Quality + Testing assignment submission for the 2026 parking fee calculator brief.

## Solution Layout

- `SQTProject2026Classic.sln`  
  Main Visual Studio solution to open for the assignment.
- `ParkingFeeCalculator/`  
  Refactored system under test containing `ParkingService` and the injectable `IDiscountService`.
- `ParkingFeeCalculator.Tests/`  
  NUnit + Moq unit tests for the main logic.
- `ParkingFeeCalculator.Web/`  
  Simple Razor Pages front end for manual testing and Selenium automation.
- `ParkingFeeCalculator.SeleniumTests/`  
  Selenium WebDriver NUnit tests for UI-level checks.
- `artifacts/`  
  Assignment write-up files and supporting documents.

## Build And Run

Open `SQTProject2026Classic.sln` in Visual Studio.

Or use the CLI:

```powershell
dotnet restore .\SQTProject2026Classic.sln
dotnet build .\SQTProject2026Classic.sln
dotnet test .\ParkingFeeCalculator.Tests\ParkingFeeCalculator.Tests.csproj
```

Run the web application:

```powershell
dotnet run --project .\ParkingFeeCalculator.Web\ParkingFeeCalculator.Web.csproj
```

## Test Projects

### Unit Tests

The unit tests check:

- standard and electric pricing rules
- invalid input
- discount application for 10+ hours
- constructor dependency validation
- case-insensitive vehicle type handling

### Selenium Tests

The Selenium tests automate the web form using NUnit.

Notes:

- a desktop browser must be installed locally for the UI tests to run
- the tests start the Razor Pages app automatically
- Selenium may also need a local browser driver setup depending on the machine

## Assignment Deliverables Included

- refactored SUT with dependency injection
- automated NUnit + Moq tests
- web front end
- Selenium test project
- control flow graph
- EP/BVA analysis
- metrics report and interpretation

## Remaining Manual Submission Items

- record the short demo video
- upload the full solution to Moodle
- if needed, install a browser locally before running Selenium from Visual Studio
- add contribution details if required by the brief

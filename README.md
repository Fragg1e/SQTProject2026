# SQTProject2026

Software Quality + Testing assignment submission for the 2026 parking fee calculator brief.

## Solution Layout

- `SQTProject2026Classic.sln`  
  Main Visual Studio solution to open for the assignment.
- `ParkingFeeCalculator/`  
  Refactored system under test containing `ParkingService` and the injectable `IDiscountService`.
- `ParkingFeeCalculator.Tests/`  
  NUnit + Moq unit tests for white-box and black-box coverage.
- `ParkingFeeCalculator.Web/`  
  Razor Pages front end for manual testing and Selenium automation.
- `ParkingFeeCalculator.SeleniumTests/`  
  Selenium WebDriver NUnit tests for UI-level black-box checks.
- `artifacts/`  
  Assignment write-up files including the control flow graph, EP/BVA tables, and metrics report.

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

The unit tests validate:

- branch coverage across standard and electric pricing rules
- invalid input handling
- discount application for 10+ hours
- constructor dependency validation
- case-insensitive vehicle type handling

### Selenium Tests

The Selenium tests automate the web form using NUnit.

Notes:

- a desktop browser must be installed locally for the UI tests to run
- the tests start the Razor Pages app automatically
- if Selenium tests are run on Azure DevOps, use a Windows agent with a browser available

## Assignment Deliverables Included

- refactored SUT with dependency injection
- automated NUnit + Moq tests
- web front end
- Selenium test project
- Azure DevOps pipeline YAML
- control flow graph
- EP/BVA analysis
- metrics report and interpretation

## Remaining Manual Submission Items

- record the short demo video
- upload the full solution to Moodle
- if needed, install a browser locally before running Selenium from Visual Studio

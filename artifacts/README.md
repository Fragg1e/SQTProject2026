# Assignment Artifacts

This folder contains the written evidence requested by the Software Quality and Testing assignment brief.

## Contents

- `control-flow-graph.md` - control-flow graph and white-box branch coverage mapping for `ParkingService.CalculateFee`.
- `ep-bva-tables.md` - equivalence partitioning and boundary value analysis tables, plus the black-box test suite.
- `metrics-report.md` - code quality metrics and interpretation for the refactored solution.
- `demo-video-script.md` - short demo video structure covering refactoring, testing, CI, and metrics.

## Verification

The unit test suite was run with:

```powershell
dotnet test .\ParkingFeeCalculator.Tests\ParkingFeeCalculator.Tests.csproj
```

Result: 17 tests passed, 0 failed.

# EP And BVA Tables

This is for the `ParkingService.CalculateFee(int hours, string vehicleType)` method.

I used equivalence partitioning to split the inputs into groups that should behave the same way. I then used boundary value analysis for the points where the answer changes, for example 3 to 4 hours, 5 to 6 hours, and 9 to 10 hours.

## Equivalence Partitioning

| ID | Input | Group / class | Valid? | Example used | Expected result |
| --- | --- | --- | --- | --- | --- |
| EP01 | Hours | Zero hours | No | `0` | `EUR0.00` |
| EP02 | Hours | Negative hours | No | `-1` | `EUR0.00` |
| EP03 | Hours + vehicle | Standard car for 1 to 3 hours | Yes | `2`, `standard` | `EUR8.00` |
| EP04 | Hours + vehicle | Standard car for 4 to 9 hours | Yes | `4`, `standard` | `EUR12.00` |
| EP05 | Hours + vehicle | Standard car for 10+ hours | Yes | `10`, `standard` | `EUR27.00` |
| EP06 | Hours + vehicle | Electric car for 1 to 5 hours | Yes | `3`, `electric` | `EUR9.00` |
| EP07 | Hours + vehicle | Electric car for 6 to 9 hours | Yes | `6`, `electric` | `EUR12.00` |
| EP08 | Hours + vehicle | Electric car for 10+ hours | Yes | `10`, `electric` | `EUR18.00` |
| EP09 | Vehicle type | Standard vehicle type | Yes | `standard` | Standard rules are used |
| EP10 | Vehicle type | Electric vehicle type | Yes | `electric` | Electric rules are used |
| EP11 | Vehicle type | Correct type but different casing/spaces | Yes | `  EleCTric  ` | Electric rules are still used |
| EP12 | Vehicle type | Empty vehicle type | No | `""` | `EUR0.00` |
| EP13 | Vehicle type | Only spaces | No | `" "` | `EUR0.00` |
| EP14 | Vehicle type | Random unsupported vehicle | No | `motorbike` | `EUR0.00` |

## Boundary Value Analysis

| ID | Boundary checked | Values | Expected result | Why I picked this |
| --- | --- | --- | --- | --- |
| BVA01 | Invalid hours to valid hours | `-1`, `0`, `1` | `-1` and `0` give `EUR0.00`; `1` starts charging | Checks the lowest valid hour |
| BVA02 | Standard first rate band | `1`, `3` | `EUR4.00`, `EUR12.00` | These are the edges of the 1 to 3 hour rule |
| BVA03 | Standard changes rate | `3`, `4` | `EUR12.00`, `EUR12.00` | Standard rate changes after 3 hours |
| BVA04 | Electric first rate band | `1`, `5` | `EUR3.00`, `EUR15.00` | These are the edges of the 1 to 5 hour rule |
| BVA05 | Electric changes rate | `5`, `6` | `EUR15.00`, `EUR12.00` | Electric rate changes after 5 hours |
| BVA06 | Discount starts | `9`, `10` | No discount at `9`; discount at `10` | The discount rule starts at 10 hours |

## Black-Box Test Suite

These are the main black-box tests I would use. Some are repeated in the automated unit tests and a smaller set is used in Selenium.

| Test | Type | Hours | Vehicle type | Expected fee | What it checks |
| --- | --- | ---: | --- | ---: | --- |
| BB01 | EP | `-1` | `standard` | `EUR0.00` | Negative hours |
| BB02 | EP/BVA | `0` | `electric` | `EUR0.00` | Zero hours |
| BB03 | BVA | `1` | `standard` | `EUR4.00` | First valid standard hour |
| BB04 | BVA | `3` | `standard` | `EUR12.00` | End of standard 4 euro rate |
| BB05 | BVA | `4` | `standard` | `EUR12.00` | Start of standard 3 euro rate |
| BB06 | BVA | `9` | `standard` | `EUR27.00` | Just before discount |
| BB07 | BVA | `10` | `standard` | `EUR27.00` | Discount starts for standard |
| BB08 | BVA | `1` | `electric` | `EUR3.00` | First valid electric hour |
| BB09 | BVA | `5` | `electric` | `EUR15.00` | End of electric 3 euro rate |
| BB10 | BVA | `6` | `electric` | `EUR12.00` | Start of electric 2 euro rate |
| BB11 | BVA | `9` | `electric` | `EUR18.00` | Just before discount |
| BB12 | BVA | `10` | `electric` | `EUR18.00` | Discount starts for electric |
| BB13 | EP | `4` | `motorbike` | `EUR0.00` | Invalid vehicle |
| BB14 | EP | `4` | `""` | `EUR0.00` | Empty vehicle |
| BB15 | EP | `4` | `" "` | `EUR0.00` | Spaces instead of a vehicle type |
| BB16 | EP | `6` | `  EleCTric  ` | `EUR12.00` | Case-insensitive input |

## Selenium Tests Used

I did not put every single case into Selenium because that would make the UI tests slower and repetitive. The Selenium tests just prove the form works with the main types of cases.

| Input in form | Expected result shown | Reason |
| --- | ---: | --- |
| `3`, `standard` | `EUR12.00` | Standard boundary case |
| `10`, `standard` | `EUR27.00` | Standard with discount |
| `6`, `electric` | `EUR12.00` | Electric boundary case |
| `4`, `motorbike` | `EUR0.00` | Invalid vehicle type |
| `0`, `electric` | `EUR0.00` | Invalid hours |

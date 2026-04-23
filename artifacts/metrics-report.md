# Metrics Report

This report is for the refactored `ParkingFeeCalculator` code. I mainly looked at `ParkingService` and `DiscountService`, because those are the actual system under test. The web project and test projects are not really the main part of this metrics section.

I worked these values out from the code rather than using a full metrics plugin. They are close enough for explaining the design and the effect of the refactoring.

## Quick Summary

The metrics are mostly fine for a small assignment project. `CalculateFee` is the most complicated method, but that is expected because it has to check the vehicle type, hours, and discount rule.

The main design improvement is that `ParkingService` no longer creates `DiscountService` by itself. It now gets an `IDiscountService` passed into the constructor, which makes it much easier to test with Moq.

## Metrics Table

| Code area | Cyclomatic Complexity | CBO | LCOM | DIT | My comment |
| --- | ---: | ---: | ---: | ---: | --- |
| `ParkingService.CalculateFee` | 5 | 1 | 0 | 1 | Highest one, but still okay |
| `ParkingService.CalculateStandardFee` | 2 | 0 | 0 | 1 | Simple helper method |
| `ParkingService.CalculateElectricFee` | 2 | 0 | 0 | 1 | Simple helper method |
| `DiscountService.GetDiscount` | 1 | 0 | 0 | 1 | Very simple |
| `ParkingService` overall | Low / medium | 1 | 0 | 1 | Fine for this project |
| `DiscountService` overall | Low | 0 | 0 | 1 | Fine |

## What The Metrics Mean

| Metric | Meaning | What I was looking for |
| --- | --- | --- |
| Cyclomatic Complexity | How many different paths there are through the code | Not too high, and covered by tests |
| CBO | How many other classes a class depends on | Lower is better |
| LCOM | Whether the methods in a class belong together | Lower is better |
| DIT | How deep the inheritance is | Low is better for this simple project |

## Cyclomatic Complexity

`CalculateFee` has the highest complexity because most of the decisions happen there. It checks invalid input, works out if the vehicle is standard or electric, and then checks if the discount should apply.

I do not think this is a problem because the method is still short and the tests cover the important branches. Splitting the standard and electric calculations into helper methods also stops the main method from becoming too messy.

## Coupling

The original code from the brief created a `DiscountService` directly inside `CalculateFee`. That made the method harder to test because the test could not easily replace the discount service.

In my version, `ParkingService` depends on `IDiscountService` instead. This is better because the unit tests can use a mocked discount service. It also means the discount could be changed later without changing the main calculator code as much.

The coupling is still not zero, because `ParkingService` obviously needs the discount service for the 10 hour rule, but it is low enough.

## Cohesion

The cohesion is okay. `ParkingService` is about calculating parking fees, and the helper methods are also about pricing. `DiscountService` only returns the discount value, so it is very focused.

## Inheritance

There is no complicated inheritance in this solution. The classes basically just inherit from `object`, so the DIT is 1. This is good here because inheritance would probably make this project more complicated than it needs to be.

## Did The Refactoring Help?

Yes, mainly because it made the code easier to test. The biggest change was moving from this idea:

`ParkingService` creates `DiscountService` itself.

to this:

`ParkingService` is given an `IDiscountService`.

That means Moq can be used to check when the discount is called. It also made it easier to test the 10 hour discount rule separately from the rest of the parking calculation.

The refactoring did not remove all the branches, but it was never going to because the parking rules are branch-based. It just made the branches easier to read and test.

## Conclusion

Overall I think the metric results are acceptable. The project is small, the classes are not heavily coupled, and there is no deep inheritance. The main improvement from refactoring is testability, especially around the discount service.

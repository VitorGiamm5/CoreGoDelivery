using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Xunit.Assert;

namespace CoreGoDelivery.ApplicationTests.Services.Internal.Rental.Commands.Create.Common;

[TestClass()]
public class CalculateDatesByPlanTests
{
    [Fact]
    public void Calculate_ShouldReturnRentalEntity_WithCorrectDates()
    {
        var service = new RentalCalculateDatesByPlan();

        var plan = new RentalPlanEntity { DaysQuantity = 5 };

        var result = RentalCalculateDatesByPlan.Calculate(plan);

        Assert.NotNull(result);
        Assert.Equal(DateTime.UtcNow.Date, result.StartDate.Date);
        Assert.Equal(DateTime.UtcNow.AddDays(plan.DaysQuantity).Date, result.EndDate.Date);
        Assert.Equal(DateTime.UtcNow.AddDays(plan.DaysQuantity).Date, result.EstimatedReturnDate.Date);
    }
}
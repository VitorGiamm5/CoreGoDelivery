using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Xunit.Assert;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common.Tests
{
    [TestClass()]
    public class CalculateDatesByPlanTests
    {
        [Fact]
        public void Calculate_ShouldReturnRentalEntity_WithCorrectDates()
        {
            var service = new CalculateDatesByPlan();

            var plan = new RentalPlanEntity { DaysQuantity = 5 };

            var result = service.Calculate(plan);

            Assert.NotNull(result);
            Assert.Equal(DateTime.UtcNow.Date, result.StartDate.Date);
            Assert.Equal(DateTime.UtcNow.AddDays(plan.DaysQuantity).Date, result.EndDate.Date);
            Assert.Equal(DateTime.UtcNow.AddDays(plan.DaysQuantity).Date, result.EstimatedReturnDate.Date);
        }
    }
}
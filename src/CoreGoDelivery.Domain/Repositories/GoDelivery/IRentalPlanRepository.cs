using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery;

public interface IRentalPlanRepository
{
    Task<RentalPlanEntity?> GetById(int id);
}

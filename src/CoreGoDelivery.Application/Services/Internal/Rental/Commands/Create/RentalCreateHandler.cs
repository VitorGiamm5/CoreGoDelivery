using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create
{
    public class RentalCreateHandler : RentalServiceBase, IRequestHandler<RentalCreateCommand, ApiResponse>
    {
        public RentalCreateHandler(
            IRentalRepository repositoryRental,
            IRentalPlanRepository repositoryPlan,
            IMotocycleRepository repositoryMotocyle,
            IDeliverierRepository repositoryDeliverier,
            IBaseInternalServices baseInternalServices)
            : base(
                  repositoryRental,
                  repositoryPlan,
                  repositoryMotocyle,
                  repositoryDeliverier,
                  baseInternalServices)
        {
        }

        public async Task<ApiResponse> Handle(RentalCreateCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await BuilderCreateValidator(request)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var plan = await _repositoryPlan.GetById(request.PlanId);
            var calculatedDates = CalculateDatesByPlan(plan!);

            var rental = MapCreateToEntity(request, calculatedDates);

            var resultCreate = await _repositoryRental.Create(rental);

            apiReponse.Message = _baseInternalServices.FinalMessageBuild(resultCreate, apiReponse);

            return apiReponse;
        }
    }
}

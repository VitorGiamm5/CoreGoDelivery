using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create
{
    public class RentalCreateHandler : IRequestHandler<RentalCreateCommand, ApiResponse>
    {
        public readonly IBaseInternalServices _baseInternalServices;
        public readonly IRentalPlanRepository _repositoryPlan;
        public readonly IRentalRepository _repositoryRental;

        public readonly RentalCreateValidate _validator;
        public readonly CalculateDatesByPlan _calculateDatesByPlan;
        public readonly RentalCreateMappers _mappers;

        public async Task<ApiResponse> Handle(RentalCreateCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await _validator.BuilderCreateValidator(request)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var plan = await _repositoryPlan.GetById(request.PlanId);

            var calculatedDates = _calculateDatesByPlan.Calculate(plan!);

            var rental = _mappers.MapCreateToEntity(request, calculatedDates);

            var resultCreate = await _repositoryRental.Create(rental);

            apiReponse.Message = _baseInternalServices.FinalMessageBuild(resultCreate, apiReponse);

            return apiReponse;
        }
    }
}

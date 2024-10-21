using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update
{
    public class RentalReturnedToBaseDateHandler : RentalServiceBase, IRequestHandler<RentalReturnedToBaseDateCommand, ApiResponse>
    {
        public RentalReturnedToBaseDateHandler(
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

        public async Task<ApiResponse> Handle(RentalReturnedToBaseDateCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await BuilderUpdateValidator(request)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var rental = await _repositoryRental.GetByIdAsync(request.Id);

            var returnedDate = request!.ReturnedToBaseDate!.Value;

            string result = BuildCalculatePenalty(returnedDate, rental) ?? CommomMessagesConst.MESSAGE_INVALID_DATA;

            var successUpdate = await _repositoryRental.UpdateReturnedToBaseDate(request.Id, returnedDate);

            if (!successUpdate)
            {
                apiReponse.Message = $"Error: fail to update {nameof(request.ReturnedToBaseDate)}; ";

                return apiReponse;
            }

            apiReponse.Data = new { messagem = result };

            return apiReponse;
        }
    }
}

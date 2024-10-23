using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update.Common;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update
{
    public class RentalReturnedToBaseDateHandler : IRequestHandler<RentalReturnedToBaseDateCommand, ApiResponse>
    {
        public readonly IRentalRepository _repositoryRental;

        public readonly RentalReturnedToBaseValidator _validator;
        public readonly CalculateDatesByPlan _calculateDatesByPlan;
        public readonly CalculatePenalty _calculatePenalty;

        public RentalReturnedToBaseDateHandler(
            IRentalRepository repositoryRental,
            RentalReturnedToBaseValidator validator,
            CalculateDatesByPlan calculateDatesByPlan,
            CalculatePenalty calculatePenalty)
        {
            _repositoryRental = repositoryRental;
            _validator = validator;
            _calculateDatesByPlan = calculateDatesByPlan;
            _calculatePenalty = calculatePenalty;
        }

        public async Task<ApiResponse> Handle(RentalReturnedToBaseDateCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await _validator.BuilderUpdateValidator(request)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var rental = await _repositoryRental.GetByIdAsync(request.Id);

            var returnedDate = request!.ReturnedToBaseDate!.Value;

            string result = _calculatePenalty.Calculate(returnedDate, rental) ?? CommomMessagesConst.MESSAGE_INVALID_DATA;

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

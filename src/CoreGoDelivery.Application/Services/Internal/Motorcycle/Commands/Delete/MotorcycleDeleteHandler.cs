using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Delete
{
    public class MotorcycleDeleteHandler : MotorcycleServiceBase, IRequestHandler<MotorcycleDeleteCommand, ApiResponse>
    {
        public MotorcycleDeleteHandler(
            IMotocycleRepository repositoryMotorcycle,
            IModelMotocycleRepository repositoryModelMotorcycle,
            IRentalRepository rentalRepository,
            IBaseInternalServices baseInternalServices)
        : base(
            repositoryMotorcycle,
            repositoryModelMotorcycle,
            rentalRepository,
            baseInternalServices)
        {
        }

        public async Task<ApiResponse> Handle(MotorcycleDeleteCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Message = await BuilderDeleteValidator(request.Id)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            _ = await _repositoryMotorcycle.DeleteById(request.Id);

            return apiReponse!;
        }
    }
}

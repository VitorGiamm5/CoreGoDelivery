using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create
{
    public class MotorcycleCreateHandler : MotorcycleServiceBase, IRequestHandler<MotorcycleCreateCommand, ApiResponse>
    {
        public MotorcycleCreateHandler(
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

        public async Task<ApiResponse> Handle(MotorcycleCreateCommand request, CancellationToken cancellationToken)
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

            var motocycle = MapCreateToEntity(request);

            var resultCreate = await _repositoryMotorcycle.Create(motocycle);

            apiReponse!.Message = _baseInternalServices.FinalMessageBuild(resultCreate, apiReponse);

            await SendNotification(motocycle);

            return apiReponse;
        }
    }
}

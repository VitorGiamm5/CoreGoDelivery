using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.GetOne
{
    public class MotorcycleGetOneQueryHandler : MotorcycleServiceBase, IRequestHandler<MotorcycleGetOneQueryCommand, ApiResponse>
    {
        public MotorcycleGetOneQueryHandler(
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

        public async Task<ApiResponse> Handle(MotorcycleGetOneQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await _repositoryMotorcycle.GetOneByIdAsync(request.Id);

            var motocycleDtos = result != null ? MapEntityToDto(result) : null;

            var apiReponse = new ApiResponse()
            {
                Data = motocycleDtos,
                Message = result == null ? "Moto não encontrada" : null
            };

            return apiReponse;
        }
    }
}

using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Publisher;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.GetOne
{
    public class MotorcycleGetOneQueryHandler : MotorcycleServiceBase, IRequestHandler<MotorcycleGetOneQueryCommand, ApiResponse>
    {
        public MotorcycleGetOneQueryHandler(
            IMotocycleRepository repositoryMotorcycle,
            IModelMotocycleRepository repositoryModelMotorcycle,
            IRentalRepository rentalRepository,
            IBaseInternalServices baseInternalServices, 
            RabbitMQPublisher publisher) 
            : base(
                  repositoryMotorcycle, 
                  repositoryModelMotorcycle, 
                  rentalRepository, 
                  baseInternalServices, 
                  publisher)
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

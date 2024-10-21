﻿using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Publisher;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Delete
{
    public class MotorcycleDeleteHandler : MotorcycleServiceBase, IRequestHandler<MotorcycleDeleteCommand, ApiResponse>
    {
        public MotorcycleDeleteHandler(
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

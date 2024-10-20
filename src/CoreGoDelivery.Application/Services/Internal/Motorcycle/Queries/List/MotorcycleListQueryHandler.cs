﻿using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List
{
    public class MotorcycleListQueryHandler : MotorcycleServiceBase, IRequestHandler<MotorcycleListQueryCommand, ApiResponse>
    {
        public MotorcycleListQueryHandler(
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

        public async Task<ApiResponse> Handle(MotorcycleListQueryCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = CommomMessagesService.MESSAGE_INVALID_DATA
            };

            request.Plate = _baseInternalServices.RemoveCharacteres(request.Plate);

            var result = await _repositoryMotorcycle.List(request?.Plate);

            if ((result == null || result?.Count == 0) && !string.IsNullOrEmpty(request?.Plate))
            {
                return apiReponse;
            }

            var motocycleDtos = MapEntityListToDto(result);

            apiReponse.Data = motocycleDtos;
            apiReponse.Message = null;

            return apiReponse;
        }
    }
}

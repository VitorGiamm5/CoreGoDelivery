using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List;

public class MotorcycleListQueryHandler : IRequestHandler<MotorcycleListQueryCommand, ApiResponse>
{
    public readonly IBaseInternalServices _baseInternalServices;
    public readonly IMotorcycleRepository _repositoryMotorcycle;

    private readonly MotorcycleServiceMappers _mapper;

    public MotorcycleListQueryHandler(
        IBaseInternalServices baseInternalServices,
        IMotorcycleRepository repositoryMotorcycle,
        MotorcycleServiceMappers mapper)
    {
        _baseInternalServices = baseInternalServices;
        _repositoryMotorcycle = repositoryMotorcycle;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(MotorcycleListQueryCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ApiResponse()
        {
            Data = null,
            Message = CommomMessagesConst.MESSAGE_INVALID_DATA
        };

        request.Plate = _baseInternalServices.RemoveCharacteres(request.Plate);

        var result = await _repositoryMotorcycle.List(request?.Plate);

        if ((result == null || result?.Count == 0) && !string.IsNullOrEmpty(request?.Plate))
        {
            return apiReponse;
        }

        var motorcycleDtos = _mapper.MapEntityListToDto(result);

        apiReponse.Data = motorcycleDtos;
        apiReponse.Message = null;

        return apiReponse;
    }
}

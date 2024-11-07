using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List;

public class MotorcycleListQueryHandler : IRequestHandler<MotorcycleListQueryCommand, ActionResult>
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

    public async Task<ActionResult> Handle(MotorcycleListQueryCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        request.Plate = _baseInternalServices.RemoveCharacteres(request.Plate);

        var result = await _repositoryMotorcycle.List(request.Plate);

        if (result == null || result.Count == 0)
        {
            return apiReponse;
        }

        var motorcycleDtos = _mapper.MapEntityListToDto(result);

        apiReponse.Data = motorcycleDtos;

        return apiReponse;
    }
}

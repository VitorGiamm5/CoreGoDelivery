using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List;

public class MotorcycleListQueryHandler : IRequestHandler<MotorcycleListQueryCommand, ActionResult>
{
    public readonly IBaseInternalServices _baseInternalServices;
    public readonly IMotorcycleRepository _repositoryMotorcycle;

    public MotorcycleListQueryHandler(
        IBaseInternalServices baseInternalServices,
        IMotorcycleRepository repositoryMotorcycle)
    {
        _baseInternalServices = baseInternalServices;
        _repositoryMotorcycle = repositoryMotorcycle;
    }

    public async Task<ActionResult> Handle(MotorcycleListQueryCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        request.Plate.RemoveCharacters();

        var result = await _repositoryMotorcycle.List(request.Plate);

        if (result == null || result.Count == 0)
        {
            return apiReponse;
        }

        var motorcycleDtos = MotorcycleServiceMappers.MapEntityListToDto(result);

        apiReponse.Data = motorcycleDtos;

        return apiReponse;
    }
}

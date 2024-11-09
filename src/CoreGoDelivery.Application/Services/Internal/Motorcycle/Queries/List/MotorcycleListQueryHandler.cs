using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Commons;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List;

public class MotorcycleListQueryHandler : IRequestHandler<MotorcycleListQueryCommand, ActionResult>
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;

    public MotorcycleListQueryHandler(
        IMotorcycleRepository repositoryMotorcycle)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
    }

    public async Task<ActionResult> Handle(MotorcycleListQueryCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        request.Plate.RemoveCharactersToUpper();

        var result = await _repositoryMotorcycle.List(request.Plate);

        if (result == null || result.Count == 0)
        {
            var emtyListMotorcycle = new List<MotorcycleEntity>();

            apiReponse.SetData(emtyListMotorcycle);

            return apiReponse;
        }

        var motorcycleDtos = MotorcycleServiceMappers.MapEntityListToDto(result);

        apiReponse.SetData(motorcycleDtos);

        return apiReponse;
    }
}

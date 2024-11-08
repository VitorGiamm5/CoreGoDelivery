using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.GetOne;

public class MotorcycleGetOneQueryHandler : IRequestHandler<MotorcycleGetOneQueryCommand, ActionResult>
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;

    public MotorcycleGetOneQueryHandler(IMotorcycleRepository repositoryMotorcycle)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
    }

    public async Task<ActionResult> Handle(MotorcycleGetOneQueryCommand request, CancellationToken cancellationToken)
    {
        var result = await _repositoryMotorcycle.GetOneByIdAsync(request.Id);

        var motorcycleDtos = result != null ? MotorcycleServiceMappers.MapEntityToDto(result) : null;

        var apiReponse = new ActionResult()
        {
            Data = motorcycleDtos,
        };

        apiReponse.SetErrorMessage(result == null ? CommomMessagesConst.MESSAGE_DATA_NOT_FOUND : null);

        return apiReponse;
    }
}

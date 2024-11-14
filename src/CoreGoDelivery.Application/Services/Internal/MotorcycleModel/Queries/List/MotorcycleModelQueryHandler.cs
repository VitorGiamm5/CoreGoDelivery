using CoreGoDelivery.Domain.Entities.GoDelivery.MotorcycleModel;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.MotorcycleModel.Queries.List;

public class MotorcycleModelQueryHandler : IRequestHandler<MotorcycleModelQueryCommand, ActionResult>
{
    public readonly IMotorcycleModelRepository _repositoryMotorcycleModel;

    public MotorcycleModelQueryHandler(IMotorcycleModelRepository repositoryMotorcycleModel)
    {
        _repositoryMotorcycleModel = repositoryMotorcycleModel;
    }

    public async Task<ActionResult> Handle(MotorcycleModelQueryCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        List<MotorcycleModelEntity> result = await _repositoryMotorcycleModel.GetAll(request.Name, request.Id);

        apiReponse.SetPaginedData(result, 1, 1, 1);

        return apiReponse;
    }
}

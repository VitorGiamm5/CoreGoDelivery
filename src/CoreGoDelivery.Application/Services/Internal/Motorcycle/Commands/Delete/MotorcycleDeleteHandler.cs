using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Delete;

public class MotorcycleDeleteHandler : IRequestHandler<MotorcycleDeleteCommand, ActionResult>
{
    public readonly IMotorcycleRepository _repositoryMotorcycle;

    private readonly MotorcycleDeleteValidator _validator;

    public MotorcycleDeleteHandler(
        IMotorcycleRepository repositoryMotorcycle,
        MotorcycleDeleteValidator validator)
    {
        _repositoryMotorcycle = repositoryMotorcycle;
        _validator = validator;
    }

    public async Task<ActionResult> Handle(MotorcycleDeleteCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();
        apiReponse.SetError(await _validator.BuilderDeleteValidator(request.Id));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        _ = await _repositoryMotorcycle.DeleteById(request.Id);

        return apiReponse!;
    }
}

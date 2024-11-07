using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create;

public class RentalCreateHandler : IRequestHandler<RentalCreateCommand, ActionResult>
{
    public readonly IBaseInternalServices _baseInternalServices;
    public readonly IRentalPlanRepository _repositoryPlan;
    public readonly IRentalRepository _repositoryRental;

    public readonly RentalCreateValidate _validator;
    public readonly RentalCalculateDatesByPlan _calculateDatesByPlan;
    public readonly RentalCreateMappers _mappers;

    public RentalCreateHandler(
        IBaseInternalServices baseInternalServices,
        IRentalPlanRepository repositoryPlan,
        IRentalRepository repositoryRental,
        RentalCreateValidate validator,
        RentalCalculateDatesByPlan calculateDatesByPlan,
        RentalCreateMappers mappers)
    {
        _baseInternalServices = baseInternalServices;
        _repositoryPlan = repositoryPlan;
        _repositoryRental = repositoryRental;
        _validator = validator;
        _calculateDatesByPlan = calculateDatesByPlan;
        _mappers = mappers;
    }

    public async Task<ActionResult> Handle(RentalCreateCommand request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();
        apiReponse.SetMessage(await _validator.BuilderCreateValidator(request));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        var plan = await _repositoryPlan.GetById(request.PlanId);

        var calculatedDates = _calculateDatesByPlan.Calculate(plan!);

        var rental = _mappers.MapCreateToEntity(request, calculatedDates);

        var resultCreate = await _repositoryRental.Create(rental);

        apiReponse.SetMessage(_baseInternalServices.FinalMessageBuild(resultCreate, apiReponse));

        return apiReponse;
    }
}

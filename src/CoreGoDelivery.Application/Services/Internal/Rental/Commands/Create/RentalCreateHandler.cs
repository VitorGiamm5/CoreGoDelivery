﻿using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
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

    public RentalCreateHandler(
        IBaseInternalServices baseInternalServices,
        IRentalPlanRepository repositoryPlan,
        IRentalRepository repositoryRental,
        RentalCreateValidate validator)
    {
        _baseInternalServices = baseInternalServices;
        _repositoryPlan = repositoryPlan;
        _repositoryRental = repositoryRental;
        _validator = validator;
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

        var calculatedDates = RentalCalculateDatesByPlan.Calculate(plan!);

        var rental = RentalCreateMappers.MapCreateToEntity(request, calculatedDates);

        var isSuccess = await _repositoryRental.Create(rental);

        if (!isSuccess)
        {
            apiReponse.SetMessage(nameof(_repositoryRental.Create).AppendError(AdditionalMessageEnum.CreateFail));
        }

        return apiReponse;
    }
}

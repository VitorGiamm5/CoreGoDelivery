﻿using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using MediatR;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne
{
    public class RentalGetOneHandler : RentalServiceBase, IRequestHandler<RentalGetOneCommand, ApiResponse>
    {
        public RentalGetOneHandler(
            IRentalRepository repositoryRental,
            IRentalPlanRepository repositoryPlan,
            IMotocycleRepository repositoryMotocyle,
            IDeliverierRepository repositoryDeliverier,
            IBaseInternalServices baseInternalServices)
        : base(
            repositoryRental,
            repositoryPlan,
            repositoryMotocyle,
            repositoryDeliverier,
            baseInternalServices)
        {
        }

        public async Task<ApiResponse> Handle(RentalGetOneCommand request, CancellationToken cancellationToken)
        {
            var message = new StringBuilder();
            var idRental = request?.Id;

            RentalEntity? rental = null;

            if (!_baseInternalServices.RequestIdParamValidator(idRental))
            {
                message.AppendError(message, "idRental");
            }
            else
            {
                rental = await _repositoryRental.GetByIdAsync(idRental!);

                if (rental == null)
                {
                    message.AppendError(message, "idRental", AdditionalMessageEnum.NotFound);
                }
            }

            var rentalDto = new
            {
                identificador = rental!.Id,
                valor_diaria = rental.RentalPlan!.DayliCost,
                entregador_id = rental.DeliverierId,
                moto_id = rental.MotorcycleId,
                data_inicio = rental.StartDate,
                data_termino = rental.EndDate,
                data_previsao_termino = rental.EstimatedReturnDate,
                data_devolucao = rental.ReturnedToBaseDate,
            };

            var apiReponse = new ApiResponse()
            {
                Data = rentalDto,
                Message = _baseInternalServices.BuildMessageValidator(message)
            };

            return apiReponse;
        }
    }
}

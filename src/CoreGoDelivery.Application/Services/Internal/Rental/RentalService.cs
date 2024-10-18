using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Rental;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental
{
    public class RentalService : RentalServiceBase, IRentalService
    {
        public RentalService(
            IRentalRepository repositoryRental,
            IRentalPlanRepository repositoryPlan,
            IMotocycleRepository repositoryMotocyle,
            IDeliverierRepository repositoryDeliverier)
            : base(repositoryRental, repositoryPlan, repositoryMotocyle, repositoryDeliverier)
        {
        }

        public async Task<ApiResponse> Create(RentalDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await BuilderCreateValidator(data)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var plan = await _repositoryPlan.GetById(data.PlanId);

            var calculatedDates = CalculateDatesByPlan(plan!);

            var rental = MapCreateToEntity(data, calculatedDates);

            var resultCreate = await _repositoryRental.Create(rental);

            apiReponse.Message = FinalMessageBuild(resultCreate, apiReponse);

            return apiReponse;
        }

        public async Task<ApiResponse> GetById(string? id)
        {
            var message = new StringBuilder();
            RentalEntity? rental = null;

            if (!RequestIdParamValidator(id))
            {
                message.AppendError(message, id, AdditionalMessageEnum.Required);
            }
            else
            {
                rental = await _repositoryRental.GetByIdAsync(id!);

                if (rental == null)
                {
                    message.AppendError(message, id, AdditionalMessageEnum.NotFound);
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
                Message = message != null ? message.ToString() : null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> UpdateReturnedToBaseDate(string id, ReturnedToBaseDateDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await BuilderUpdateValidator(id, data)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var rental = await _repositoryRental.GetByIdAsync(id);

            var returnedDate = data!.ReturnedToBaseDate!.Value;

            string result = BuildCalculatePenalty(returnedDate, rental) ?? MESSAGE_INVALID_DATA;

            var successUpdate = await _repositoryRental.UpdateReturnedToBaseDate(id, returnedDate);

            if (!successUpdate)
            {
                apiReponse.Message = $"Error: fail to update {nameof(data.ReturnedToBaseDate)}; ";
                return apiReponse;
            }

            apiReponse.Data = new { messagem = result };

            return apiReponse;
        }
    }
}

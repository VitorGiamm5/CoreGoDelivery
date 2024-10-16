using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Rental;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental
{
    public class RentalService : RentalServiceBase, IRentalService
    {
        private readonly IRentalRepository _repositoryRental;
        private readonly IRentalPlanRepository _repositoryPlan;
        private readonly IMotocycleRepository _repositoryMotocyle;
        private readonly IDeliverierRepository _repositoryDeliverier;

        public RentalService(
            IRentalRepository repositoryRental,
            IRentalPlanRepository repositoryPlan,
            IMotocycleRepository repositoryMotocyle,
            IDeliverierRepository repositoryDeliverier)
        {
            _repositoryRental = repositoryRental;
            _repositoryPlan = repositoryPlan;
            _repositoryMotocyle = repositoryMotocyle;
            _repositoryDeliverier = repositoryDeliverier;
        }

        public async Task<ApiResponse> Create(RentalDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await ValidatorCreateAsync(data)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var plan = await _repositoryPlan.GetById(data.PlanId);

            var calculatedDates = CalculateDatesByPlan(plan!);

            var rental = CreateToEntity(data, calculatedDates);

            var resultCreate = await _repositoryRental.Create(rental);

            apiReponse.Message = FinalMessageBuild(resultCreate, apiReponse);

            return apiReponse;
        }

        public async Task<ApiResponse> GetById(string id)
        {
            var results = _repositoryRental.GetById(id);

            var apiReponse = new ApiResponse()
            {
                Data = results,
                Message = null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> UpdateReturnedToBaseDate(string id, ReturnedToBaseDateDto data)
        {
            DateTime dateTime = data.ReturnedToBaseDate ?? (DateTime)data.ReturnedToBaseDate;

            var success = await _repositoryRental.UpdateReturnedToBaseDate(id, dateTime);

            var results = new ResponseMessageDto()
            {
                Message = success
                    ? "Data de devolução informada com sucesso"
                    : "Dados inválidos"
            };

            var apiReponse = new ApiResponse()
            {
                Data = results,
                Message = null
            };

            return apiReponse;
        }

        public async Task<bool> CheckMotorcycleIsAvaliavleById(string id)
        {
            var motocycleIsAvaliable = await _repositoryRental.FindByMotorcycleId(id);

            return motocycleIsAvaliable == null;
        }

        #region Private

        private async Task<string?> ValidatorCreateAsync(RentalDto data)
        {
            var message = new StringBuilder();

            #region PlanId validator
            if (string.IsNullOrWhiteSpace(data.PlanId.ToString()))
            {
                message.Append($"Empty: {nameof(data.PlanId)}; ");
            }
            else
            {
                var plan = await _repositoryPlan.GetById(data.PlanId);
                if (plan == null)
                {
                    message.Append($"Invalid: {nameof(data.PlanId)}: {data.PlanId} not exist; ");
                }
                else
                {
                    var resultValidateDates = ValidadeToPlan(ref data, plan);
                    if (!string.IsNullOrWhiteSpace(resultValidateDates))
                    {
                        message.Append(resultValidateDates);
                    }
                }
            }
            #endregion

            #region MotorcycleId validator
            if (string.IsNullOrWhiteSpace(data.MotorcycleId))
            {
                message.Append($"Empty: {nameof(data.MotorcycleId)}; ");
            }
            else
            {
                var existMotocycleId = await _repositoryMotocyle.GetOneById(data.MotorcycleId);
                if (existMotocycleId == null)
                {
                    message.Append($"Invalid: {nameof(data.MotorcycleId)}: {data.MotorcycleId} not exist; ");
                }

                var motocycleIsAvaliable = await CheckMotorcycleIsAvaliavleById(data.MotorcycleId);
                if (!motocycleIsAvaliable)
                {
                    message.Append($"Invalid: {nameof(data.MotorcycleId)}: {data.MotorcycleId} is not avaliable; ");
                }
            }
            #endregion

            #region DeliverierId validator
            if (string.IsNullOrWhiteSpace(data.DeliverierId))
            {
                message.Append($"Empty:  {nameof(data.DeliverierId)}; ");
            }
            else
            {
                var existDeliverierId = !await _repositoryDeliverier.CheckIsUnicById(data.DeliverierId);
                if (!existDeliverierId)
                {
                    message.Append($"Invalid: {nameof(data.DeliverierId)}: {data.DeliverierId} not exist; ");
                }
            }
            #endregion

            return message.ToString();
        }

        #endregion
    }
}

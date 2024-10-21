using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental
{
    public class RentalServiceBase
    {
        public readonly IRentalRepository _repositoryRental;
        public readonly IRentalPlanRepository _repositoryPlan;
        public readonly IMotocycleRepository _repositoryMotocyle;
        public readonly IDeliverierRepository _repositoryDeliverier;
        public readonly IBaseInternalServices _baseInternalServices;

        public RentalServiceBase(
            IRentalRepository repositoryRental,
            IRentalPlanRepository repositoryPlan,
            IMotocycleRepository repositoryMotocyle,
            IDeliverierRepository repositoryDeliverier,
            IBaseInternalServices baseInternalServices)
        {
            _repositoryRental = repositoryRental;
            _repositoryPlan = repositoryPlan;
            _repositoryMotocyle = repositoryMotocyle;
            _repositoryDeliverier = repositoryDeliverier;
            _baseInternalServices = baseInternalServices;
        }

        #region Mappers



        #endregion

        /// <summary>
        /// used to Update
        /// </summary>
        /// <param name="data"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
 



        /// <summary>
        /// used to Update
        /// </summary>
        /// <param name="data"></param>
        /// <param name="plan"></param>
        /// <returns></returns>

        /// <summary>
        /// used to: CalculateDatesByPlan
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        public static RentalEntity CalculateDatesByPlan(RentalPlanEntity plan)
        {
            var result = new RentalEntity()
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(plan.DaysQuantity),
                EstimatedReturnDate = DateTime.UtcNow.AddDays(plan.DaysQuantity)
            };

            return result;
        }


    }
}
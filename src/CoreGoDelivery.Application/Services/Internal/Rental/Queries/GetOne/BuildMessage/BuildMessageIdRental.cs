using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne.BuildMessage
{
    public class BuildMessageIdRental
    {
        public readonly IBaseInternalServices _baseInternalServices;
        public readonly IRentalRepository _repositoryRental;

        public BuildMessageIdRental(IBaseInternalServices baseInternalServices, IRentalRepository repositoryRental)
        {
            _baseInternalServices = baseInternalServices;
            _repositoryRental = repositoryRental;
        }

        public async Task<RentalEntity?> Build(StringBuilder message, string? idRental, RentalEntity? rental)
        {
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

            return rental;
        }
    }
}

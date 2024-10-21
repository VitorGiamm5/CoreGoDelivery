using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne.BuildMessage;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne
{
    public class RentalGetOneHandler : IRequestHandler<RentalGetOneCommand, ApiResponse>
    {
        public readonly IBaseInternalServices _baseInternalServices;
        public readonly IRentalRepository _repositoryRental;

        public readonly RentalGetOneMappers _mapper;
        public readonly BuildMessageIdRental _buildMessageIdRental;

        public RentalGetOneHandler(
            IBaseInternalServices baseInternalServices,
            IRentalRepository repositoryRental,
            RentalGetOneMappers mapper, 
            BuildMessageIdRental buildMessageIdRental)
        {
            _baseInternalServices = baseInternalServices;
            _repositoryRental = repositoryRental;
            _mapper = mapper;
            _buildMessageIdRental = buildMessageIdRental;
        }

        public async Task<ApiResponse> Handle(RentalGetOneCommand request, CancellationToken cancellationToken)
        {
            var message = new StringBuilder();

            var idRental = request?.Id;

            RentalEntity? rental = null;

            rental = await _buildMessageIdRental.Build(message, idRental, rental);

            var rentalDto = _mapper.RentalEntityToDto(rental);

            var apiReponse = new ApiResponse()
            {
                Data = rentalDto,
                Message = _baseInternalServices.BuildMessageValidator(message)
            };

            return apiReponse;
        }
    }
}

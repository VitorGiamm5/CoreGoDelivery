using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById
{
    public class MotorcycleChangePlateHandler : IRequestHandler<MotorcycleChangePlateCommand, ApiResponse>
    {
        public readonly IMotocycleRepository _repositoryMotorcycle;
        public readonly IModelMotocycleRepository _repositoryModelMotorcycle;
        public readonly IRentalRepository _rentalRepository;
        public readonly IBaseInternalServices _baseInternalServices;

        public readonly MotorcycleChangePlateValidator _validator;

        public MotorcycleChangePlateHandler(
            IMotocycleRepository repositoryMotorcycle,
            IModelMotocycleRepository repositoryModelMotorcycle,
            IRentalRepository rentalRepository,
            IBaseInternalServices baseInternalServices,
            MotorcycleChangePlateValidator validator)
        {
            _repositoryMotorcycle = repositoryMotorcycle;
            _repositoryModelMotorcycle = repositoryModelMotorcycle;
            _rentalRepository = rentalRepository;
            _baseInternalServices = baseInternalServices;
            _validator = validator;
        }

        public async Task<ApiResponse> Handle(MotorcycleChangePlateCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await _validator.ChangePlateValidator(request.Id, request.Plate)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var plateNormalized = _baseInternalServices.RemoveCharacteres(request.Plate);

            var success = await _repositoryMotorcycle.ChangePlateByIdAsync(request.Id, plateNormalized);

            apiReponse.Data = success ? new { mensagem = CommomMessagesConst.MESSAGE_UPDATED_PLATE_SUCCESS } : null;

            apiReponse.Message = success ? null : CommomMessagesConst.MESSAGE_INVALID_DATA;

            return apiReponse;
        }
    }
}

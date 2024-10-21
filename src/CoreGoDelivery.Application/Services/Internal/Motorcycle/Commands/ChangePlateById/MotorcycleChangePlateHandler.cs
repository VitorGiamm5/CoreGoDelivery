using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById
{
    public class MotorcycleChangePlateHandler : MotorcycleServiceBase, IRequestHandler<MotorcycleChangePlateCommand, ApiResponse>
    {
        public MotorcycleChangePlateHandler(
            IMotocycleRepository repositoryMotorcycle,
            IModelMotocycleRepository repositoryModelMotorcycle,
            IRentalRepository rentalRepository,
            IBaseInternalServices baseInternalServices)
        : base(
            repositoryMotorcycle,
            repositoryModelMotorcycle,
            rentalRepository,
            baseInternalServices)
        {
        }

        public async Task<ApiResponse> Handle(MotorcycleChangePlateCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = await ChangePlateValidator(request.Id, request.Plate)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            var plateNormalized = _baseInternalServices.RemoveCharacteres(request.Plate);

            var success = await _repositoryMotorcycle.ChangePlateByIdAsync(request.Id, plateNormalized);

            apiReponse.Data = success ? new { mensagem = "Placa modificada com sucesso" } : null;

            apiReponse.Message = success ? null : CommomMessagesService.MESSAGE_INVALID_DATA;

            return apiReponse;
        }
    }
}

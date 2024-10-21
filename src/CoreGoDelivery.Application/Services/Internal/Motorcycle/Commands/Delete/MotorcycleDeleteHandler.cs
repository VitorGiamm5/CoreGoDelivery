using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Delete
{
    public class MotorcycleDeleteHandler : IRequestHandler<MotorcycleDeleteCommand, ApiResponse>
    {
        public readonly IMotocycleRepository _repositoryMotorcycle;

        private readonly MotorcycleDeleteValidator _validator;

        public MotorcycleDeleteHandler(
            IMotocycleRepository repositoryMotorcycle,
            MotorcycleDeleteValidator validator)
        {
            _repositoryMotorcycle = repositoryMotorcycle;
            _validator = validator;
        }

        public async Task<ApiResponse> Handle(MotorcycleDeleteCommand request, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse()
            {
                Message = await _validator.BuilderDeleteValidator(request.Id)
            };

            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return apiReponse;
            }

            _ = await _repositoryMotorcycle.DeleteById(request.Id);

            return apiReponse!;
        }
    }
}

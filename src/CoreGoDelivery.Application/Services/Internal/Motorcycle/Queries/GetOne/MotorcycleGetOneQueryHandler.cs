using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.GetOne
{
    public class MotorcycleGetOneQueryHandler : IRequestHandler<MotorcycleGetOneQueryCommand, ApiResponse>
    {
        public readonly IMotorcycleRepository _repositoryMotorcycle;

        private readonly MotorcycleServiceMappers _mapper;

        public MotorcycleGetOneQueryHandler(IMotorcycleRepository repositoryMotorcycle, MotorcycleServiceMappers mapper)
        {
            _repositoryMotorcycle = repositoryMotorcycle;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(MotorcycleGetOneQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await _repositoryMotorcycle.GetOneByIdAsync(request.Id);

            var motorcycleDtos = result != null ? _mapper.MapEntityToDto(result) : null;

            var apiReponse = new ApiResponse()
            {
                Data = motorcycleDtos,
                Message = result == null ? CommomMessagesConst.MESSAGE_MOTORCYCLE_NOT_FOUND : null
            };

            return apiReponse;
        }
    }
}

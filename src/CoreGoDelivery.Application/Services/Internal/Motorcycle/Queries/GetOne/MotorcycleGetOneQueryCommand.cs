using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.GetOne
{
    public class MotorcycleGetOneQueryCommand : IRequest<ApiResponse>
    {
        public string Id { get; set; }
    }
}

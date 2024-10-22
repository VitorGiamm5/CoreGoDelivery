using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne
{
    public class RentalGetOneCommand : IRequest<ApiResponse>
    {
        public string? Id { get; set; }
    }
}

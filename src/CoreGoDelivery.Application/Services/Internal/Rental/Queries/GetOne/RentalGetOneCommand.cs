using CoreGoDelivery.Domain.DTO.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne
{
    public class RentalGetOneCommand : IRequest<ApiResponse>
    {
        public RentalGetOneCommand(string? id)
        {
            Id = id;
        }

        public string? Id { get; set; }
    }
}

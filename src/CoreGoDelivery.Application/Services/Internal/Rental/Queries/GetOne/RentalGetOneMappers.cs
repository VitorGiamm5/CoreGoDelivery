using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne;

public static class RentalGetOneMappers
{
    public static object? RentalEntityToDto(RentalEntity? rental)
    {
        if (rental == null)
        {
            return null;
        }

        var restult = new
        {
            identificador = rental.Id,
            valor_diaria = rental.RentalPlan!.DayliCost,
            entregador_id = rental.DeliverierId,
            moto_id = rental.MotorcycleId,
            data_inicio = rental.StartDate,
            data_termino = rental.EndDate,
            data_previsao_termino = rental.EstimatedReturnDate,
            data_devolucao = rental.ReturnedToBaseDate,
        };

        return restult;
    }
}

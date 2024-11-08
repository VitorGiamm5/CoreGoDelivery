﻿using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Response;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update.Common;

public static class RentalCalculatePenalty
{
    public static ActionResult Calculate(DateTime returnedToBaseDate, RentalEntity? rental)
    {
        var apiResponse = new ActionResult();

        if (rental == null)
        {
            apiResponse.SetError(nameof(returnedToBaseDate).AppendError(AdditionalMessageEnum.NotFound));

            return apiResponse;
        }

        TimeSpan buildDiffDays = returnedToBaseDate - rental!.EstimatedReturnDate;

        int diffDays = buildDiffDays.Days;

        if (diffDays == 0)
        {
            apiResponse.SetData(new { Message = RentalServiceConst.MESSAGE_RETURNED_TO_BASE_SUCCESS });

            return apiResponse;
        }

        if (diffDays < 0)
        {
            var valueToPay = RentalReturnerBeforeExpected.Calculate(rental, diffDays);

            apiResponse.SetData(new { Message = $"Value to pay: {RentalServiceConst.CURRENCY_BRL} {valueToPay}" });

            return apiResponse;
        }
        else
        {
            var valueToPay = RentalExpiredDateToReturn.Calculate(diffDays);

            apiResponse.SetData(new { Menssage = $"Value to pay: {RentalServiceConst.CURRENCY_BRL} {valueToPay}" });

            return apiResponse;
        }
    }
}

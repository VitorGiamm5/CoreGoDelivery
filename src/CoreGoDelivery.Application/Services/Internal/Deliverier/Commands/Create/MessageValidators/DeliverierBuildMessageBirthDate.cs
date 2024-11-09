using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Consts;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;

public static class DeliverierBuildMessageBirthDate
{
    public static void Build(DeliverierCreateCommand data, StringBuilder message)
    {
        var paramName = nameof(data.BirthDate);

        if (string.IsNullOrWhiteSpace(data.BirthDate.ToString()))
        {
            message.Append(paramName.AppendError());
        }
        else
        {
            if (data.BirthDate is DateTime birthDate)
            {
                var age = DateTime.Today.Year - birthDate.Year;

                if (birthDate.Date > DateTime.Today.AddYears(-age))
                {
                    age--;
                }

                if (age < 18)
                {
                    message.Append(DeliverierServiceConst.MESSAGE_INVALID_AGE);
                }
            }
        }
    }
}

namespace CoreGoDelivery.Domain.Enums.ServiceErrorMessage
{
    public static class ValidatosServicesMessagesEnumExtensions
    {
        public static string GetMessage(this AdditionalMessageEnum value)
        {
            return value switch
            {
                AdditionalMessageEnum.None => "",
                AdditionalMessageEnum.InvalidFormat => "invalid format",
                AdditionalMessageEnum.Required => "required",
                AdditionalMessageEnum.MustBeUnic => "must be unic",
                AdditionalMessageEnum.Unavailable => "unavailable",
                AdditionalMessageEnum.AlreadyExist => "already exist",
                AdditionalMessageEnum.NotFound => "not found",
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }
    }
}

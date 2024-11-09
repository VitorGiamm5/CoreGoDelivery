namespace CoreGoDelivery.Domain.Enums.ServiceErrorMessage;

public static class ValidatosServicesMessagesEnumExtensions
{
    public static string GetMessage(this AdditionalMessageEnum value)
    {
        return value switch
        {
            AdditionalMessageEnum.None => "",
            AdditionalMessageEnum.InvalidFormat => "format file is not valid",
            AdditionalMessageEnum.FileSizeInvalid => "the file is not suported",
            AdditionalMessageEnum.InvalidDate => "date is invalid",
            AdditionalMessageEnum.UpdateFail => "fail to update, verify connection",
            AdditionalMessageEnum.CreateFail => "fail to create, verify connection",
            AdditionalMessageEnum.Required => "required",
            AdditionalMessageEnum.MustBeUnic => "must be unic",
            AdditionalMessageEnum.Unavailable => "unavailable",
            AdditionalMessageEnum.AlreadyExist => "already exist",
            AdditionalMessageEnum.NotFound => "not found",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}

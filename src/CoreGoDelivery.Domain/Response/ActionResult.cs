
using System.Text;

namespace CoreGoDelivery.Domain.Response;

public sealed class ActionResult
{
    public object? Data { get; set; } = null;

    public ErrorDetails? Error { get; set; } = null;

    public class ErrorDetails
    {
        public string? Message { get; set; } = null;
        public object? Details { get; set; } = null;
    }

    public bool HasError()
    {
        return !string.IsNullOrWhiteSpace(Error?.Message);
    }

    public bool HasDataType()
    {
        return Data != null && !string.IsNullOrEmpty(Data.GetType().ToString());
    }

    public void SetMessage(StringBuilder? stringBuilder)
    {
        Error!.Message = stringBuilder?.ToString();
    }

    public void SetMessage(string? message)
    {
        Error!.Message = message;
    }
}

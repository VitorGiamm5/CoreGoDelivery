using System.Text;

namespace CoreGoDelivery.Domain.Response;

public sealed class ActionResult
{
    public object? Data { get; set; } = null;

    public ErrorDetails? Error { get; set; } = null;

    public class ErrorDetails
    {
        public string? Message { get; set; }
        public object? Details { get; set; }
    }

    public bool HasError()
    {
        return !string.IsNullOrWhiteSpace(Error?.Message);
    }

    public bool HasDataType()
    {
        return Data != null && !string.IsNullOrEmpty(Data.GetType().ToString());
    }

    public void SetMessage(StringBuilder stringBuilder)
    {
        SetError(stringBuilder.ToString());
    }

    public void SetErrorMessage(string message)
    {
        SetError(message);
    }

    public void SetError(string message)
    {
        Error = new ErrorDetails { Message = message };
    }

    public void SetError(string message, object? details)
    {
        Error = new ErrorDetails { Message = message, Details = details };
    }
}

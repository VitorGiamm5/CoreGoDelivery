using System.Text;

namespace CoreGoDelivery.Domain.Response;

public sealed class ActionResult
{
    private object? Data { get; set; } = null;

    private ErrorDetails? Error { get; set; } = null;

    public class ErrorDetails
    {
        public string? Message { get; set; }
        public object? Details { get; set; }
    }

    public bool HasError()
    {
        return !string.IsNullOrWhiteSpace(Error?.Message) || Error?.Details != null;
    }

    public bool HasData()
    {
        return Data != null;
    }

    public void SetError(object message, object? details = null)
    {
        Data = null;
        Error = new ErrorDetails
        {
            Message = message is StringBuilder sb ? sb.ToString() : message?.ToString(),
            Details = details
        };
    }

    public void SetData(object data)
    {
        Data = data;
    }

    public object GetData()
    {
        return new { Data };
    }

    public object GetError()
    {
        return new { Error };
    }
}

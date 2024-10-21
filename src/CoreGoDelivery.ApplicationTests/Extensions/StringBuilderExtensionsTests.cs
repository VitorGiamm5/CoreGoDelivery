using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using System.Text;
using Xunit;

namespace CoreGoDelivery.ApplicationTests.Extensions
{
    //public class StringBuilderExtensionsTests
    //{
    //    [Theory]
    //    [InlineData(null, "cnpj", AdditionalMessageEnum.InvalidFormat, "Invalid field: 'paramName', type: System.String, value: '', Detail: 'invalid format'; ")]
    //    [InlineData("12345678901234", "cnpj", AdditionalMessageEnum.None, "Invalid field: 'paramName', type: System.String, value: '12345678901234', Detail: ''; ")]
    //    [InlineData(null, "birthDate", AdditionalMessageEnum.Required, "Invalid field: 'paramName', type: System.String, value: '', Detail: 'required'; ")]
    //    [InlineData("2024-10-13", "birthDate", AdditionalMessageEnum.MustBeUnic, "Invalid field: 'paramName', type: System.String, value: '2024-10-13', Detail: 'must be unic'; ")]
    //    public void AppendError_ShouldAppendExpectedMessage(object data, object paramName, AdditionalMessageEnum additionalMessage, string expected)
    //    {
    //        var message = new StringBuilder();

    //        message.AppendError(data, paramName, additionalMessage);
    //        var messageString = message.ToString();

    //        Assert.Equal(expected, messageString);
    //    }
    //}
}
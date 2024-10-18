using CoreGoDelivery.Application.Services.Internal;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using System.Text;
using Xunit;

namespace CoreGoDelivery.Application.Extensions.Tests
{
    public class StringBuilderExtensionsTests
    {
        [Theory]
        [InlineData(null, "cnpj", AdditionalMessageEnum.InvalidFormat, "InvalidFormat field: cnpj, Type: String, Value:null Detail: invalid;")]
        [InlineData("12345678901234", "cnpj", AdditionalMessageEnum.None, "InvalidFormat field: cnpj, Type: String, Value:12345678901234 Detail: ;")]
        [InlineData(null, "birthDate", AdditionalMessageEnum.Required, "InvalidFormat field: birthDate, Type: DateTime, Value:null Detail: required;")]
        [InlineData("2024-10-13", "birthDate", AdditionalMessageEnum.MustBeUnic, "InvalidFormat field: birthDate, Type: String, Value:2024-10-13 Detail: must be unic;")]
        public void AppendError_ShouldAppendExpectedMessage(object data, object paramName, AdditionalMessageEnum additionalMessage, string expected)
        {
            var message = new StringBuilder();

            message.AppendError(data, paramName, additionalMessage);

            Assert.Equal(expected, message.ToString());
        }
    }
}
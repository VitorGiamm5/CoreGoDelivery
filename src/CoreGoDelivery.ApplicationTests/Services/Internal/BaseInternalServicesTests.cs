using CoreGoDelivery.Application.Services.Internal;
using CoreGoDelivery.Domain.DTO.Response;
using System.Text;
using Xunit;

namespace CoreGoDelivery.ApplicationTests.Services.Internal
{
    public class BaseInternalServicesTests
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("existing-id", false)]
        public void SelectId_ShouldReturnCorrectId(string input, bool expectNewId)
        {
            var result = BaseInternalServices.IdBuild(input);

            if (expectNewId)
            {
                Assert.True(Ulid.TryParse(result, out _));
            }
            else
            {
                Assert.Equal(input, result);
            }
        }

        [Theory]
        [InlineData("ABC-1234", "ABC1234")]
        [InlineData("ABC 1234", "ABC1234")]
        [InlineData("ABC.1234", "ABC1234")]
        [InlineData("ABC,1234", "ABC1234")]
        [InlineData(null, "")]
        [InlineData("", "")]
        public void RemoveCharacteres_ShouldRemoveSpecialCharactersAndUpperCase(string input, string expected)
        {
            var result = BaseInternalServices.RemoveCharacteres(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(true, null, null)]
        [InlineData(false, null, "Dados inválidos")]
        [InlineData(true, "Error processing request", null)]
        [InlineData(false, "Error processing request", null)]
        public void FinalMessageBuild_ShouldReturnExpectedMessage(bool resultCreate, string apiMessage, string expected)
        {
            var apiReponse = new ApiResponse { Message = apiMessage };

            var result = BaseInternalServices.FinalMessageBuild(resultCreate, apiReponse);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Error: Missing plate", "Error: Missing plate")]
        [InlineData("Warning: Year is too old", "Warning: Year is too old")]
        [InlineData("", null)]
        [InlineData("Error: InvalidFormat model name", "Error: InvalidFormat model name")]
        public void BuildMessageValidator_ShouldReturnCorrectMessage(string inputMessage, string expected)
        {
            var message = new StringBuilder(inputMessage);

            var result = BaseInternalServices.BuildMessageValidator(message);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Error occurred", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void HasMessageError_ShouldReturnCorrectResult(string message, bool expected)
        {
            var apiResponse = new ApiResponse { Message = message };

            var result = BaseInternalServices.HasMessageError(apiResponse);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(":id", true)]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("validId", false)]
        [InlineData("12345", false)]
        public void RequestIdParamValidator_ShouldReturnCorrectResult(string id, bool expected)
        {
            var result = BaseInternalServices.RequestIdParamValidator(id);

            Assert.Equal(expected, result);
        }
    }
}
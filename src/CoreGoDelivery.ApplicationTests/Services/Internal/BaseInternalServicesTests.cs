using CoreGoDelivery.Application.Services.Internal;
using CoreGoDelivery.Domain.DTO.Response;
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
        [InlineData(true, "Erro ao processar a requisição", null)]
        [InlineData(false, "Erro ao processar a requisição", null)]
        public void FinalMessageBuild_ShouldReturnExpectedMessage(bool resultCreate, string apiMessage, string expected)
        {
            var apiReponse = new ApiResponse { Message = apiMessage };

            var result = BaseInternalServices.FinalMessageBuild(resultCreate, apiReponse);

            Assert.Equal(expected, result);
        }
    }
}
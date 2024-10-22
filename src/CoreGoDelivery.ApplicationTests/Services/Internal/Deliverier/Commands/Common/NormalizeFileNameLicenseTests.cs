using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using Xunit;
using Assert = Xunit.Assert;


namespace CoreGoDelivery.ApplicationTests.Services.Internal.Deliverier.Commands.Common
{
    public class NormalizeFileNameLicenseTests
    {
        [Theory]
        [InlineData("123456789", "CNH_123456789.png")]
        [InlineData("987654321", "CNH_987654321.png")]
        [InlineData("", "CNH_.png")] // Caso de uma string vazia
        [InlineData(null, "CNH_.png")] // Caso de null
        public void Normalize_ShouldReturnCorrectFileName_WhenGivenLicenseNumber(string licenseNumber, string expectedFileName)
        {
            // Arrange
            var command = new DeliverierCreateCommand
            {
                LicenseNumber = licenseNumber
            };
            var yourClass = new NormalizeFileNameLicense();

            // Act
            var result = yourClass.Normalize(command.LicenseNumber, FileExtensionEnum.none);

            // Assert
            Assert.Equal(expectedFileName, result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var yourClass = new YourClass(); // Supondo que o método Normalize está nessa classe

            // Act
            var result = yourClass.Normalize(command);

            // Assert
            Assert.Equal(expectedFileName, result);
        }
    }
}

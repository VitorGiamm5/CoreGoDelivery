using CoreGoDelivery.Application.Services.Internal.Deliverier;
using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using Xunit;
using Assert = Xunit.Assert;

namespace CoreGoDelivery.ApplicationTests.Services.Internal.Deliverier
{
    public class DeliverierServiceBaseTests
    {
        [Theory]
        [InlineData("1234567890", "CNH_1234567890.png")]
        [InlineData("0987654321", "CNH_0987654321.png")]
        [InlineData("", "CNH_.png")]
        [InlineData("ABC123", "CNH_ABC123.png")]
        public void FileNameNormalize_ShouldFormatFileNameCorrectly(string licenseNumber, string expected)
        {
            var data = new DeliverierDto { LicenseNumber = licenseNumber };

            var result = DeliverierServiceBase.FileNameNormalize(data);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("A", LicenseTypeEnum.A)]
        [InlineData("AB", LicenseTypeEnum.AB)]
        [InlineData("None", LicenseTypeEnum.None)]
        [InlineData("a", LicenseTypeEnum.A)]
        [InlineData("ab", LicenseTypeEnum.AB)]
        [InlineData("", LicenseTypeEnum.None)]
        public void ParseLicenseType_ShouldReturnCorrectEnumValue(string input, LicenseTypeEnum expected)
        {
            var data = new DeliverierDto { LicenseType = input };

            var result = DeliverierServiceBase.ParseLicenseType(data);

            Assert.Equal(expected, result);
        }
    }
}
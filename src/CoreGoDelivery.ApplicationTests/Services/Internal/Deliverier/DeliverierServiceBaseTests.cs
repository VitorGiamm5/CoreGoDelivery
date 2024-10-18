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
        [InlineData("123", "John Doe", "12.345.678/0001-95", "1985-05-15", "AB", "123456789", "CNH_123456789.png")]
        [InlineData("456", "Jane Smith", "98.765.432/0001-76", "1990-10-01", "A", "987654321", "CNH_987654321.png")]
        [InlineData("789", "Alan Turing", "11.111.111/0001-11", "1912-06-23", "AB", "111111111", "CNH_111111111.png")]
        [InlineData("012", "Ada Lovelace", "22.222.222/0002-22", "1815-12-10", "A", "222222222", "CNH_222222222.png")]
        public void CreateToEntity_ShouldCreateDeliverierEntityCorrectly(
            string id, string fullName, string cnpj, string birthDate, 
            string licenseType, string licenseNumber, string expectedImageUrl)
        {
            var data = new DeliverierDto
            {
                Id = id,
                FullName = fullName,
                CNPJ = cnpj,
                BirthDate = DateTime.Parse(birthDate),
                LicenseType = licenseType,
                LicenseNumber = licenseNumber
            };

            var result = DeliverierServiceBase.MapCreateToEntity(data);

            Assert.Equal(id, result.Id);
            Assert.Equal(fullName, result.FullName);
            Assert.Equal(DeliverierServiceBase.RemoveCharacteres(cnpj), result.CNPJ);
            Assert.Equal(DateTime.Parse(birthDate), result.BirthDate);
            Assert.Equal(licenseNumber, result.LicenceDriver.Id);
            Assert.Equal(DeliverierServiceBase.ParseLicenseType(data), result.LicenceDriver.Type);
            Assert.Equal(expectedImageUrl, result.LicenceDriver.ImageUrlReference);
        }

        [Theory]
        [InlineData("1234567890", "CNH_1234567890.png")]
        [InlineData("0987654321", "CNH_0987654321.png")]
        [InlineData("", "CNH_.png")]
        [InlineData("ABC123", "CNH_ABC123.png")]
        public void FileNameNormalize_ShouldFormatFileNameCorrectly(string licenseNumber, string expected)
        {
            var data = new DeliverierDto { LicenseNumber = licenseNumber };

            var result = DeliverierServiceBase.FileNameLicenseNormalize(data);

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
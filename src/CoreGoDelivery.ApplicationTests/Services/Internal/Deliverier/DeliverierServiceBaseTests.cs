using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using System.Text;
using Xunit;
using Assert = Xunit.Assert;

namespace CoreGoDelivery.ApplicationTests.Services.Internal.Deliverier
{
    public class DeliverierServiceBaseTests
    {
        //[Theory]
        //[InlineData("123", "John Doe", "12.345.678/0001-95", "1985-05-15", "AB", "123456789", "CNH_123456789.png")]
        //[InlineData("456", "Jane Smith", "98.765.432/0001-76", "1990-10-01", "A", "987654321", "CNH_987654321.png")]
        //[InlineData("789", "Alan Turing", "11.111.111/0001-11", "1912-06-23", "AB", "111111111", "CNH_111111111.png")]
        //[InlineData("012", "Ada Lovelace", "22.222.222/0002-22", "1815-12-10", "A", "222222222", "CNH_222222222.png")]
        //public void CreateToEntity_ShouldCreateDeliverierEntityCorrectly(
        //    string id, string fullName, string cnpj, string birthDate,
        //    string licenseType, string licenseNumber, string expectedImageUrl)
        //{
        //    var data = new DeliverierCreateCommand
        //    {
        //        Id = id,
        //        FullName = fullName,
        //        Cnpj = cnpj,
        //        BirthDate = DateTime.Parse(birthDate),
        //        LicenseType = licenseType,
        //        LicenseNumber = licenseNumber
        //    };

        //    var result = DeliverierServiceBase.MapCreateToEntity(data);

        //    Assert.Equal(id, result.Id);
        //    Assert.Equal(fullName, result.FullName);
        //    Assert.Equal(DeliverierServiceBase.RemoveCharacteres(cnpj), result.Cnpj);
        //    Assert.Equal(DateTime.Parse(birthDate), result.BirthDate);
        //    Assert.Equal(licenseNumber, result.LicenceDriver.Id);
        //    Assert.Equal(DeliverierServiceBase.ParseLicenseType(data), result.LicenceDriver.Type);
        //    Assert.Equal(expectedImageUrl, result.LicenceDriver.ImageUrlReference);
        //}

        [Theory]
        [InlineData("1234567890", "CNH_1234567890.png")]
        [InlineData("0987654321", "CNH_0987654321.png")]
        [InlineData("", "CNH_.png")]
        [InlineData("ABC123", "CNH_ABC123.png")]
        public void FileNameNormalize_ShouldFormatFileNameCorrectly(string licenseNumber, string expected)
        {
            var data = new DeliverierCreateCommand { LicenseNumber = licenseNumber };

            var result = BuildMessageFullName.NormalizeFileNameLicense(data);

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
            var data = new DeliverierCreateCommand { LicenseType = input };

            var result = BuildMessageFullName.ParseLicenseType(data);

            Assert.Equal(expected, result);
        }

        //[Theory]
        //[InlineData(null, "Invalid field: cnpj, Type: String, Value:null Detail: ;")]
        //[InlineData("", "Invalid field: cnpj, Type: String, Value:null Detail: ;")]
        //[InlineData("12345678901234", "Invalid field: cnpj, Type: String, Value:12345678901234 Detail: ;", false)] // Cnpj inválido
        //[InlineData("12345678000195", "", true)] // Cnpj válido
        //public void BuildMessageCnpj_ShouldAppendExpectedMessage(string cnpj, string expectedMessage, bool isValid = false)
        //{
        //    // Mock da validação de Cnpj
        //    CnpjValidationMock.Setup(x => x.Validate(It.IsAny<string>())).Returns(isValid);

        //    // Arrange
        //    var message = new StringBuilder();

        //    // Act
        //    BuildMessageCnpj(message, cnpj);

        //    // Assert
        //    Assert.Equal(expectedMessage, message.ToString().Trim());
        //}
    }
}
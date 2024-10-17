using CoreGoDelivery.Application.Services.Internal.Deliverier;
using CoreGoDelivery.Domain.DTO.Deliverier;
using Xunit;
using Assert = Xunit.Assert;

namespace CoreGoDelivery.ApplicationTests.Services.Internal.Deliverier
{
    public class DeliverierServiceBaseTests
    {
        [Theory]
        [InlineData("12.345.678/0001-95", "12345678000195")]
        [InlineData("12 345 678/0001 95", "12345678000195")]
        [InlineData("12345678/0001-95", "12345678000195")]
        [InlineData("12.345.678.0001-95", "12345678000195")]
        [InlineData("12 345 678 0001 95", "12345678000195")]
        public void CnpjNormalize_ShouldRemoveSpecialCharacters(string input, string expected)
        {
            var data = new DeliverierDto { CNPJ = input };

            var result = DeliverierServiceBase.CnpjNormalize(data);

            Assert.Equal(expected, result);
        }
        /*
        [TestMethod()]
        public void CnpjNormalizeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FileNameNormalizeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FinalMessageBuildTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ParseLicenseTypeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SelectIdTest()
        {
            Assert.Fail();
        }
        */
    }
}
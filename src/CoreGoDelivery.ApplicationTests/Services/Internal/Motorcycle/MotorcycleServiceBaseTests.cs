using CoreGoDelivery.Application.Services.Internal.Motorcycle;
using CoreGoDelivery.Domain.DTO.Motorcycle;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using Xunit;

namespace CoreGoDelivery.ApplicationTests.Services.Internal.Motorcycle
{
    public class MotorcycleServiceBaseTests
    {
        [Theory]
        [InlineData("moto123", 2020, "Mottu Sport", "CDX-0101", "moto123", 2020, "Mottu Sport", "CDX0101")]
        public void CreateToEntity_ShouldConvertDtoToEntity(string id, int year, string model, string plate, string expectedId, int expectedYear, string expectedModel, string expectedPlate)
        {
            var data = new MotorcycleDto
            {
                Id = id,
                YearManufacture = year,
                ModelName = model,
                PlateId = plate
            };

            MotorcycleEntity result = MotorcycleServiceBase.CreateToEntity(data);

            Assert.Equal(expectedId, result.Id);
            Assert.Equal(expectedYear, result.YearManufacture);
            Assert.Equal(expectedModel, result.ModelMotorcycleId);
            Assert.Equal(expectedPlate, result.PlateNormalized);
        }

        //        Rastreamento de Pilha: 
        //MotorcycleServiceBaseTests.CreateToEntity_ShouldConvertDtoToEntity(String id, Int32 year, String model, String plate, String expectedId, Int32 expectedYear, String expectedModel, String expectedPlate) linha 26
        //InvokeStub_MotorcycleServiceBaseTests.CreateToEntity_ShouldConvertDtoToEntity(Object, Span`1)
        //MethodBaseInvoker.InvokeWithManyArgs(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)


        [Theory]
        [InlineData("ABC-1234", true)]
        [InlineData("ABC1234", true)]
        [InlineData("ABC1A23", true)]
        [InlineData("ABC-1A23", true)]
        [InlineData("A1B-1234", false)]
        [InlineData("123-ABCD", false)]
        [InlineData("ABC-123", false)]
        [InlineData(null, false)]
        [InlineData("     ", false)]
        public void ValidatePlate_ShouldValidatePlateCorrectly(string plateId, bool expected)
        {
            var result = MotorcycleServiceBase.ValidatePlate(plateId);

            Assert.Equal(expected, result);
        }


    }
}
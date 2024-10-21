using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commons;
using CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotorcycle;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using Xunit;

namespace CoreGoDelivery.ApplicationTests.Services.Internal.Motorcycle
{
    public class MotorcycleServiceBaseTests
    {
        //[Theory]
        //[InlineData("moto123", 2020, "Mottu Sport", "CDX-0101", "moto123", 2020, "Mottu Sport", "CDX0101")]
        //public void CreateToEntity_ShouldConvertDtoToEntity(string id, int year, string model, string plate, string expectedId, int expectedYear, string expectedModel, string expectedPlate)
        //{
        //    var data = new MotorcycleCreateCommand
        //    {
        //        IdLicense = id,
        //        YearManufacture = year,
        //        ModelName = model,
        //        PlateId = plate
        //    };

        //    MotorcycleEntity result = MotorcycleServiceBase.MapCreateToEntity(data);

        //    Assert.Equal(expectedId, result.IdLicense);
        //    Assert.Equal(expectedYear, result.YearManufacture);
        //    Assert.Equal(expectedModel, result.ModelMotorcycleId);
        //    Assert.Equal(expectedPlate, result.PlateNormalized);
        //}

        [Theory]
        [InlineData("1", 2020, "Model A", "ABC-1234")]
        [InlineData("2", 2021, "Model B", "DEF-5678")]
        [InlineData("3", 2022, "Model C", "GHI-9012")]
        [InlineData("4", 2019, "Model D", "JKL-3456")]
        public void EntityToDto_ShouldMapMotorcycleEntityToMotorcycleDto(
             string id, int yearManufacture, string modelName, string plateId)
        {
            var modelMotorcycle = new ModelMotorcycleEntity { Name = modelName };

            var motorcycleEntity = new MotorcycleEntity
            {
                Id = id,
                YearManufacture = yearManufacture,
                PlateNormalized = plateId,
                ModelMotorcycle = modelMotorcycle
            };

            var result = MotorcycleServiceBase.MapEntityToDto(motorcycleEntity);

            Assert.Equal(id, result.Id);
            Assert.Equal(yearManufacture, result.YearManufacture);
            Assert.Equal(modelName, result.ModelName);
            Assert.Equal(plateId, result.PlateId);
        }

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
            var result = PlateValidator.Validator(plateId);

            Assert.Equal(expected, result);
        }
    }
}
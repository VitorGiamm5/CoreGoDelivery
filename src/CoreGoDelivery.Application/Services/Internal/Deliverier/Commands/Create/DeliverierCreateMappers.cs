using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create
{
    public class DeliverierCreateMappers
    {
        public readonly IBaseInternalServices _baseInternalServices;

        public readonly NormalizeFileNameLicense _normalizeFileNameLicense;
        public readonly ParseLicenseType _parseLicenseType;

        public DeliverierCreateMappers(
            IBaseInternalServices baseInternalServices, 
            NormalizeFileNameLicense normalizeFileNameLicense, 
            ParseLicenseType parseLicenseType)
        {
            _baseInternalServices = baseInternalServices;
            _normalizeFileNameLicense = normalizeFileNameLicense;
            _parseLicenseType = parseLicenseType;
        }

        public DeliverierEntity MapCreateToEntity(DeliverierCreateCommand data)
        {
            var result = new DeliverierEntity()
            {
                Id = _baseInternalServices.IdBuild(data.Id),
                FullName = data.FullName,
                Cnpj = _baseInternalServices.RemoveCharacteres(data.Cnpj),
                BirthDate = data.BirthDate,
                LicenceDriver = new LicenceDriverEntity()
                {
                    Id = data.LicenseNumber,
                    Type = _parseLicenseType.Parse(data),
                    ImageUrlReference = _normalizeFileNameLicense.Normalize(data)
                }
            };

            return result;
        }
    }
}

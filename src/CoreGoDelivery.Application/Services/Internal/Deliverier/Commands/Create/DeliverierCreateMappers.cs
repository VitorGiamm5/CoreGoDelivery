using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create
{
    public class DeliverierCreateMappers
    {
        public readonly IBaseInternalServices _baseInternalServices;

        public readonly BuildFileName _buildFileName;
        public readonly BuildExtensionFile _buildExtensionFile;
        public readonly ParseLicenseType _parseLicenseType;

        public DeliverierCreateMappers(
            IBaseInternalServices baseInternalServices,
            BuildFileName buildFileName,
             BuildExtensionFile buildExtensionFile,
            ParseLicenseType parseLicenseType)
        {
            _baseInternalServices = baseInternalServices;
            _buildFileName = buildFileName;
            _buildExtensionFile = buildExtensionFile;
            _parseLicenseType = parseLicenseType;
        }

        public DeliverierEntity MapCreateToEntity(DeliverierCreateCommand command)
        {
            var (_, _, fileExtension) = _buildExtensionFile.Build(command.LicenseImageBase64);

            var fileName = _buildFileName.Build(command.LicenseNumber, fileExtension);

            var result = new DeliverierEntity()
            {
                Id = _baseInternalServices.IdBuild(command.Id),
                FullName = command.FullName,
                Cnpj = _baseInternalServices.RemoveCharacteres(command.Cnpj),
                BirthDate = command.BirthDate,
                LicenceDriver = new LicenceDriverEntity()
                {
                    Id = command.LicenseNumber,
                    Type = _parseLicenseType.Parse(command),
                    ImageUrlReference = fileName
                }
            };

            return result;
        }
    }
}
